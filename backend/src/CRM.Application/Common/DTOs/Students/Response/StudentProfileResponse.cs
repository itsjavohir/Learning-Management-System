namespace CRM.Application.Common.DTOs.Students.Response;

public record StudentProfileResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    decimal Balance
);