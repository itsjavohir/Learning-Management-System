using CRM.Application.Common.Settings;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CRM.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IVerificationChannelResolver channelResolver,
    IOptions<TelegramGatewaySettings> telegramOptions,
    ILogger<ForgotPasswordCommandHandler> logger)
    : IRequestHandler<ForgotPasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = request.Channel == VerificationChannel.Telegram
            ? await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken)
            : await unitOfWork.User.GetByEmailAsync(request.Email!, cancellationToken);

        if (user is not null)
        {
            var ttlSeconds = telegramOptions.Value.DefaultTtlSeconds;
            var destination = request.Channel == VerificationChannel.Telegram
                ? request.PhoneNumber
                : request.Email!;

            var channel = channelResolver.Resolve(request.Channel);

          
            var existingCode = await unitOfWork.VerificationCode.GetActiveCodeAsync(
                user.Id, VerificationCodeType.PasswordReset, cancellationToken);

            if (existingCode is not null)
            {
                if (!string.IsNullOrEmpty(existingCode.ProviderRequestId))
                    await channel.RevokeAsync(existingCode.ProviderRequestId, cancellationToken);

                existingCode.IsUsed = true;
                unitOfWork.VerificationCode.Update(existingCode);
            }

            var code = GenerateResetCode();
            var codeHash = passwordHasher.Hash(code);

           var sendResult = await channel.SendCodeAsync(destination, code, ttlSeconds, user.FullName, cancellationToken);

            if (!sendResult.Success)
            {
                logger.LogWarning("Send failed via {Channel} for {Destination}: {Error}",
                    request.Channel, destination, sendResult.Error);
                return Result<bool>.Ok(true);
            }

            var verificationCode = new VerificationCode
            {
                UserId = user.Id,
                CodeHash = codeHash,
                ProviderRequestId = sendResult.ProviderRequestId,
                Channel = request.Channel,
                Type = VerificationCodeType.PasswordReset,
                Expiration = DateTime.UtcNow.AddSeconds(ttlSeconds),
                IsUsed = false,
                Attempts = 0,
                MaxAttempts = 5
            };

            await unitOfWork.VerificationCode.AddAsync(verificationCode, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result<bool>.Ok(true);
    }

    private static string GenerateResetCode()
    {
        return System.Security.Cryptography.RandomNumberGenerator.GetInt32(10000, 99999).ToString();
    }
}