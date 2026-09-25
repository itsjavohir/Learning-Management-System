namespace CRM.Application.Common.DTOs.Mentors.Response;

public record MentorProfileResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string? Specialization,
    string? Bio,
    int ExperienceYears,
    string? LinkedInUrl,
    string? GithubUrl,
    DateTime HireDate,
    bool IsActive
);
