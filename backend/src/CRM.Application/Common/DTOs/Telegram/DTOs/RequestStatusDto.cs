using System.Text.Json.Serialization;

namespace CRM.Application.Common.DTOs.Telegram.DTOs;

public record TelegramGatewayResponse<T>(bool Ok, T? Result, string? Error);
public record RequestStatusDto(
    [property: JsonPropertyName("request_id")] string RequestId,
    [property: JsonPropertyName("phone_number")] string PhoneNumber,
    [property: JsonPropertyName("request_cost")] double RequestCost,
    [property: JsonPropertyName("is_refunded")] bool? IsRefunded,
    [property: JsonPropertyName("remaining_balance")] double? RemainingBalance,
    [property: JsonPropertyName("delivery_status")] DeliveryStatusDto? DeliveryStatus,
    string? Payload
);

public record DeliveryStatusDto(
    string Status,
    [property: JsonPropertyName("updated_at")] long UpdatedAt
);