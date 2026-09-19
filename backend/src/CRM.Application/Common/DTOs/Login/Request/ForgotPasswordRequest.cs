using CRM.Domain.Enums;

namespace CRM.Application.Common.DTOs.Login.Request;

public record ForgotPasswordRequest(
    string? PhoneNumber,
    string? Email,
    VerificationChannel Channel
);

