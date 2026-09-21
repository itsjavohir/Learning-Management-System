namespace CRM.Application.Common.DTOs.Groups.Response;

public record GroupResponse(
    Guid Id,
    string Name,
    Guid CourseId,
    string CourseName,
    Guid MentorId,
    string MentorFullName,
    DateTime StartDate,
    int MaxStudents
);