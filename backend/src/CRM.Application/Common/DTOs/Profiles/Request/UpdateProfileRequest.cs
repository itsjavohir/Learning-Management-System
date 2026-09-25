namespace CRM.Application.Common.DTOs.Profiles.Request;

public record UpdateProfileRequest(
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
