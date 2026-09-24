using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Mentor:BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public string? Bio { get; set; }
    public int ExperienceYears { get; set; } = 0;
    public string? LinkedInUrl { get; set; }
    public string? GithubUrl { get; set; }
    public DateTime HireDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public ICollection<Group> Groups { get; set; } = new List<Group>();
}
