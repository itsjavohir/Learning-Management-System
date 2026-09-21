namespace CRM.Application.Common.DTOs.Course.Response;

public record CourseResponse(
    Guid Id,
    string Name,
    string? Description,
    int DurationWeeks,
    bool IsActive
);
