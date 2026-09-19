using CRM.Domain.Enums;

namespace CRM.Application.Common.DTOs.Login.Request;


public record ResetPasswordRequest(
    string? PhoneNumber,
    string? Email,
    string VerifyCode,
    string NewPassword,
    string ConfirmPassword,
    VerificationChannel Channel  
);