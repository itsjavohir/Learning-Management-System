namespace CRM.Application.Common.DTOs.Login.Request;

public record  ChangePasswordRequest
(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);