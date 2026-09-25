using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public class SeasonOverride
{
    public Guid Id { get; private set; }
    public Season? Season { get; private set; }
    public Guid SetByUserId { get; private set; }
    public DateTime SetAtUtc { get; private set; }
    public DateTime? ExpiresAtUtc { get; private set; }

    private SeasonOverride() { }

    public static SeasonOverride Create(Season? season, Guid userId, DateTime? expiresAtUtc)
        => new()
        {
            Id = Guid.NewGuid(),
            Season = season,
            SetByUserId = userId,
            SetAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = expiresAtUtc
        };
}