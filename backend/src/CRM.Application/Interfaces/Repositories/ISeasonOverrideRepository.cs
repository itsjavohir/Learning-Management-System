using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface ISeasonOverrideRepository
{
    Task<SeasonOverride?> GetActiveAsync(CancellationToken cancellationToken);
    Task<List<SeasonOverride>> GetHistoryAsync(CancellationToken cancellationToken);
    Task AddAsync(SeasonOverride entity, CancellationToken cancellationToken);
}