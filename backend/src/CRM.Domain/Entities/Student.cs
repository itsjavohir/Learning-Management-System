using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Student:BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? PhotoUrl { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? TelegramUsername { get; set; }
    public string? GithubUrl { get; set; }
    public string? AboutMe { get; set; }
    public decimal Balance { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public DateTime EnrollDate { get; set; } = DateTime.UtcNow;

    public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();
}
