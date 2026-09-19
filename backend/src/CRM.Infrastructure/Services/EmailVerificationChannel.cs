using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Services;

public class EmailVerificationChannel(IEmailService emailService) : IVerificationChannel
{
    public VerificationChannel Channel => VerificationChannel.Email;

    public async Task<ChannelSendResult> SendCodeAsync(
        string destination, string code, int ttlSeconds, string? displayName, CancellationToken cancellationToken)
    {
        try
        {
            await emailService.SendPasswordResetCodeAsync(destination, displayName ?? "", code, cancellationToken);
            return new ChannelSendResult(true, null, null);
        }
        catch (Exception ex)
        {
            return new ChannelSendResult(false, null, ex.Message);
        }
    }

    public Task RevokeAsync(string providerRequestId, CancellationToken cancellationToken)
        => Task.CompletedTask;
}