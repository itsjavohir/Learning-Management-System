namespace CRM.Application.Common.DTOs.Lessons.Request;

public record CreateLessonRequest(
    int WeekNumber,
    DateTime LessonDate,
    string? Title,
    string? Description,
    string? HomeworkDescription,
    string? MaterialUrl
);
