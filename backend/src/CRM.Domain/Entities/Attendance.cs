using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public class Attendance : BaseEntity
{
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? AbsenceReason { get; set; }
    public string? MentorNote { get; set; }

    public Guid? MarkedByMentorId { get; set; }
    public Mentor? MarkedByMentor { get; set; }

    public DateTime MarkedAt { get; set; } = DateTime.UtcNow;
    public int? LateMinutes { get; set; }
}
