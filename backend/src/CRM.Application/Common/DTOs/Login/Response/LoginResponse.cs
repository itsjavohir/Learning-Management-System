namespace CRM.Application.Common.DTOs.Login.Response;

public record LoginResponse
(
    string AccessToken,
    string RefreshToken,
    bool MustChangePassword
);
