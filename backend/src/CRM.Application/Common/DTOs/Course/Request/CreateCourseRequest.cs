namespace CRM.Application.Common.DTOs.Course.Request;

public record CreateCourseRequest(
    string Name,
    string? Description,
    int DurationWeeks
);