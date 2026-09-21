namespace CRM.Application.Common.DTOs.Course.Request;

public record UpdateCourseRequest(
    string Name,
    string? Description,
    int DurationWeeks,
    bool IsActive
);

