using CRM.Domain.Entities;

namespace CRM.Application.Common.DTOs.Users.Response;

public record CreateUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string RoleName,
    string TemporaryPassword
);
