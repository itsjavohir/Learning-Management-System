using CRM.Domain.Enums;

namespace CRM.Application.Interfaces.Services;

public record ChannelSendResult(bool Success, string? ProviderRequestId, string? Error);

public interface IVerificationChannel
{
    VerificationChannel Channel { get; }
    Task<ChannelSendResult> SendCodeAsync(
        string destination, string code, int ttlSeconds, string? displayName, CancellationToken cancellationToken);
    Task RevokeAsync(string providerRequestId, CancellationToken cancellationToken);
}