using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

public class VerificationCode : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string CodeHash { get; set; } = null!;
    public string? ProviderRequestId { get; set; }   // было TelegramRequestId
    public VerificationChannel Channel { get; set; }  // новое поле
    public VerificationCodeType Type { get; set; }

    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; } = false;
    public int Attempts { get; set; } = 0;
    public int MaxAttempts { get; set; } = 5;

    public DateTime? UsedAt { get; set; }
}