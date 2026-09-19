using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Services;

public class TelegramVerificationChannel(ITelegramGatewayService telegramGatewayService) : IVerificationChannel
{
    public VerificationChannel Channel => VerificationChannel.Telegram;

    public async Task<ChannelSendResult> SendCodeAsync(
    string destination, string code, int ttlSeconds, string? displayName, CancellationToken cancellationToken)
{
    var checkResult = await telegramGatewayService.CheckSendAbilityAsync(destination, cancellationToken);
    if (!checkResult.Success)
        return new ChannelSendResult(false, null, checkResult.Error);

    var sendResult = await telegramGatewayService.SendVerificationMessageAsync(
        destination, checkResult.RequestId, code, ttlSeconds, cancellationToken);

    return new ChannelSendResult(sendResult.Success, sendResult.RequestId, sendResult.Error);
}

    public async Task RevokeAsync(string providerRequestId, CancellationToken cancellationToken)
    {
        await telegramGatewayService.RevokeVerificationMessageAsync(providerRequestId, cancellationToken);
    }
}