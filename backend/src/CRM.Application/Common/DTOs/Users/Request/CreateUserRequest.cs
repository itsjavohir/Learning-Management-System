namespace CRM.Application.Common.DTOs.Users.Request;

public record CreateUserRequest
(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    Guid RoleId
);
