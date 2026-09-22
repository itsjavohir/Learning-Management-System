using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public class ThemeSettings
{
    public Guid Id { get; private set; }
    public Season? ManualOverride { get; private set; } 
    public bool IsOverrideEnabled { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid? UpdatedByUserId { get; private set; }

    private ThemeSettings() { }

    public static ThemeSettings CreateDefault()
        => new()
        {
            Id = Guid.NewGuid(),
            IsOverrideEnabled = false,
            UpdatedAt = DateTime.UtcNow
        };

    public void SetOverride(Season season, Guid updatedByUserId)
    {
        ManualOverride = season;
        IsOverrideEnabled = true;
        UpdatedByUserId = updatedByUserId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearOverride(Guid updatedByUserId)
    {
        ManualOverride = null;
        IsOverrideEnabled = false;
        UpdatedByUserId = updatedByUserId;
        UpdatedAt = DateTime.UtcNow;
    }
}