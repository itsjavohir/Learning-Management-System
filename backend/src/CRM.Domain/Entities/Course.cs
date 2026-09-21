using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Course:BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int DurationWeeks { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Group> Groups { get; set; } = new List<Group>();
}
