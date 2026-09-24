using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Lesson : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int WeekNumber { get; set; }
    public DateTime LessonDate { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? HomeworkDescription { get; set; }
    public string? MaterialUrl { get; set; }
    public bool IsCompleted { get; set; } = false;
}
