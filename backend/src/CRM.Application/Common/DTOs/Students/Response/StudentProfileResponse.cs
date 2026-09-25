namespace CRM.Application.Common.DTOs.Students.Response;

public record StudentProfileResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    decimal Balance,
    string? PhotoUrl,
    DateTime? DateOfBirth,
    string? TelegramUsername,
    string? GithubUrl,
    string? AboutMe,
    bool IsActive,
    DateTime EnrollDate
);
