namespace CRM.Application.Common.Settings;

public class TelegramGatewaySettings
{ 
    public const string SectionName = "TelegramGateway";
    public string BaseUrl { get; set; } = "https://gatewayapi.telegram.org/";
    public string AccessToken { get; set; } = null!;
    public int DefaultTtlSeconds { get; set; } = 300; 
}
