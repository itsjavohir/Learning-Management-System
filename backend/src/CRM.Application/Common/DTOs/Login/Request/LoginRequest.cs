namespace CRM.Application.Common.DTOs.Login.Request;

public record LoginRequest
(
    string PhoneNumber,
    string Password
);