namespace CRM.Application.Common.DTOs.Login.Request;


public record ResetPasswordRequest(
    string PhoneNumber,
    string VerifyCode,
    string NewPassword,
    string ConfirmPassword
);
