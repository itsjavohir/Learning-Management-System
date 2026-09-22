using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Persistence.Repositories;

public class ThemeSettingsRepository(AppDbContext context) : IThemeSettingsRepository
{
    public Task<ThemeSettings?> GetAsync(CancellationToken cancellationToken)
        => context.ThemeSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(ThemeSettings settings, CancellationToken cancellationToken)
        => await context.ThemeSettings.AddAsync(settings, cancellationToken);
}