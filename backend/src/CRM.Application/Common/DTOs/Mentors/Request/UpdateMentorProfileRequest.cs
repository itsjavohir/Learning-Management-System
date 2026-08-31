namespace CRM.Application.Common.DTOs.Mentors.Request;

public record UpdateMentorProfileRequest
(
    string? Specialization,
    string? Bio,
    int ExperienceYears
);
