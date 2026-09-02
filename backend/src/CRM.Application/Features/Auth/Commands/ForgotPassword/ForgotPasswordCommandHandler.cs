using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(IUnitOfWork unitOfWork,IPasswordHasher passwordHasher,IEmailService emailService)
:IRequestHandler<ForgotPasswordCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(ForgotPasswordCommand command,CancellationToken cancellationToken)
    {
        
        var request = command.Request;
        var user = await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber,cancellationToken);

        if(user is not null)
        {
            
        
       var code = GenerateResetCode();
        var codeHash = passwordHasher.Hash(code);

        var verificationcode = new VerificationCode
        {
            UserId = user.Id,
            CodeHash = codeHash,
            Type = VerificationCodeType.PasswordReset,
            Expiration = DateTime.UtcNow.AddMinutes(15),
            IsUsed = false,
            Attempts = 0,
            MaxAttempts = 5
            
        };
        
        await unitOfWork.VerificationCode.AddAsync(verificationcode,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await emailService.SendPasswordResetCodeAsync(user.Email!,user.FullName,code,cancellationToken);
        }
        
        return Result<bool>.Ok(true);
        
    }
    private static string GenerateResetCode()
    {
       return System.Security.Cryptography.RandomNumberGenerator.GetInt32(10000, 99999).ToString();
    } 

    
}

