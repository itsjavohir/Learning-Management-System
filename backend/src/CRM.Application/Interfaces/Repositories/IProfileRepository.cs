using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Profile profile, CancellationToken cancellationToken);
    void Update(Profile profile);
}
