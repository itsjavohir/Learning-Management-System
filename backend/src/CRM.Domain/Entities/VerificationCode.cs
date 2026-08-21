using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public class VerificationCode : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string CodeHash { get; set; } = null!;
    public VerificationCodeType Type { get; set; }

    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; } = false;
    public int Attempts { get; set; } = 0;
    public int MaxAttempts { get; set; } = 5;

    public DateTime? UsedAt { get; set; }
}