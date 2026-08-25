namespace CRM.Application.Common.DTOs.Users.Response;

public record UserResponse
(
     Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string RoleName,
    bool IsActive
);
