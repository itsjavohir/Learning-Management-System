namespace CRM.Application.Common.DTOs.Users;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string RoleName,
    bool IsActive
);