using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; } = null!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public int MaxStudents { get; set; } = 15;

    public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();
}
