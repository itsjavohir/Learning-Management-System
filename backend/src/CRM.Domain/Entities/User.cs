using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}".Trim();

    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public string PasswordHash { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public bool MustChangePassword { get; set; } = true;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

    public Student? Student { get; set; }
    public Mentor? Mentor { get; set; }
}