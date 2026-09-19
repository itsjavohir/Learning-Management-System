using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CRM.Application.Common.DTOs.Telegram.DTOs;
using CRM.Application.Common.Settings;
using CRM.Application.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace CRM.Infrastructure.Services;

public class TelegramGatewayService : ITelegramGatewayService
{
    private readonly HttpClient _httpClient;
    private readonly TelegramGatewaySettings _settings;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public TelegramGatewayService(HttpClient httpClient, IOptions<TelegramGatewaySettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.AccessToken);
    }

    public async Task<TelegramCheckResult> CheckSendAbilityAsync(
        string phoneNumber, CancellationToken cancellationToken)
    {
        try
        {
            var payload = new { phone_number = phoneNumber };
            var response = await _httpClient.PostAsJsonAsync("checkSendAbility", payload, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new TelegramCheckResult(false, null, await response.Content.ReadAsStringAsync(cancellationToken));

            var body = await response.Content.ReadFromJsonAsync<TelegramGatewayResponse<RequestStatusDto>>(
                JsonOptions, cancellationToken);

            if (body is null)
                return new TelegramCheckResult(false, null, "Empty response");

            return body.Ok
                ? new TelegramCheckResult(true, body.Result!.RequestId, null)
                : new TelegramCheckResult(false, null, body.Error);
        }
        catch (Exception ex)
        {
            return new TelegramCheckResult(false, null, ex.Message);
        }
    }

    public async Task<TelegramSendResult> SendVerificationMessageAsync(
        string phoneNumber, string? requestId, string code, int ttlSeconds,
        CancellationToken cancellationToken)
    {
        try
        {
            var payload = new
            {
                phone_number = phoneNumber,
                request_id = requestId,   //#
                code,                      
                ttl = ttlSeconds
            };

            var response = await _httpClient.PostAsJsonAsync("sendVerificationMessage", payload, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new TelegramSendResult(false, null, await response.Content.ReadAsStringAsync(cancellationToken));

            var body = await response.Content.ReadFromJsonAsync<TelegramGatewayResponse<RequestStatusDto>>(
                JsonOptions, cancellationToken);

            if (body is null)
                return new TelegramSendResult(false, null, "Empty response");

            return body.Ok
                ? new TelegramSendResult(true, body.Result!.RequestId, null)
                : new TelegramSendResult(false, null, body.Error);
        }
        catch (Exception ex)
        {
            return new TelegramSendResult(false, null, ex.Message);
        }
    }

    public async Task<bool> RevokeVerificationMessageAsync(
        string requestId, CancellationToken cancellationToken)
    {
        try
        {
            var payload = new { request_id = requestId };
            var response = await _httpClient.PostAsJsonAsync("revokeVerificationMessage", payload, cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}