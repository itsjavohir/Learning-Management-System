namespace CRM.Application.Common.DTOs.Users.Request;

public record UpdateUserRequest
(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email
);
