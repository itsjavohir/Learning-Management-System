namespace CRM.Application.Common.DTOs.Students.Request;

public record UpdateStudentProfileRequest(
    string? PhotoUrl,
    DateTime? DateOfBirth,
    string? TelegramUsername,
    string? GithubUrl,
    string? AboutMe
);
