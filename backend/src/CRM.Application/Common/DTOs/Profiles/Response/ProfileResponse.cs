namespace CRM.Application.Common.DTOs.Profiles.Response;

public record ProfileResponse(
    Guid Id,
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? AvatarUrl,
    string? Phone,
    DateTime? DateOfBirth,
    string? Address,
    string? TelegramUsername,
    string? LinkedInUrl,
    string? GithubUrl,
    string? AboutMe
);
