public record MentorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string? Specialization,
    string? Bio,
    int ExperienceYears
);