using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class SeasonOverrideRepository(AppDbContext context) : ISeasonOverrideRepository
{
    public Task<SeasonOverride?> GetActiveAsync(CancellationToken cancellationToken)
        => context.SeasonOverrides
            .AsNoTracking()
            .Where(item => item.ExpiresAtUtc == null || item.ExpiresAtUtc > DateTime.UtcNow)
            .OrderByDescending(item => item.SetAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<List<SeasonOverride>> GetHistoryAsync(CancellationToken cancellationToken)
        => context.SeasonOverrides
            .AsNoTracking()
            .OrderByDescending(item => item.SetAtUtc)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(SeasonOverride entity, CancellationToken cancellationToken)
        => await context.SeasonOverrides.AddAsync(entity, cancellationToken);
}