namespace CRM.Application.Common.DTOs.Lessons.Response;

public record LessonResponse(
    Guid Id,
    Guid GroupId,
    string GroupName,
    int WeekNumber,
    DateTime LessonDate,
    string? Title,
    string? Description,
    string? HomeworkDescription,
    string? MaterialUrl,
    bool IsCompleted
);
