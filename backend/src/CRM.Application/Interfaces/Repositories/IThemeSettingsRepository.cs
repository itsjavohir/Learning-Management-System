using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IThemeSettingsRepository
{
    Task<ThemeSettings?> GetAsync(CancellationToken cancellationToken);
    Task AddAsync(ThemeSettings settings, CancellationToken cancellationToken);
}
