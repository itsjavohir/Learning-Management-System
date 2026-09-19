namespace CRM.Application.Interfaces.Services;
public record TelegramCheckResult(bool Success, string? RequestId, string? Error);
public record TelegramSendResult(bool Success, string? RequestId, string? Error);
public interface  ITelegramGatewayService
{
   Task<TelegramCheckResult> CheckSendAbilityAsync(
        string phoneNumber, CancellationToken cancellationToken);

    Task<TelegramSendResult> SendVerificationMessageAsync(
        string phoneNumber, string? requestId, string code, int ttlSeconds,
        CancellationToken cancellationToken);

    Task<bool> RevokeVerificationMessageAsync(
        string requestId, CancellationToken cancellationToken);
}
