namespace CRM.Application.Common.DTOs.Groups.Request;

public record CreateGroupRequest(
    string Name,
    Guid CourseId,
    Guid MentorId,
    DateTime StartDate,
    int MaxStudents
);