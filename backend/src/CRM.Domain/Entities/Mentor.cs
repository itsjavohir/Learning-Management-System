using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Mentor:BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Specialization { get; set; }
    public string? Bio { get; set; }
    public int ExperienceYears { get; set; } = 0;
}
