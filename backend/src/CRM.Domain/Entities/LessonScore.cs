using CRM.Domain.Common;

namespace CRM.Domain.Entities;

// NOTE: the pasted draft also had HomeworkSubmissionId/HomeworkSubmission,
// but there is no HomeworkSubmission entity in this codebase yet — left out
// on purpose so this compiles. Add it back once that entity exists.
public class LessonScore : BaseEntity
{
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public decimal Score { get; set; }
    public string? MentorFeedback { get; set; }

    public Guid? ScoredByMentorId { get; set; }
    public Mentor? ScoredByMentor { get; set; }

    public DateTime ScoredAt { get; set; } = DateTime.UtcNow;
}
