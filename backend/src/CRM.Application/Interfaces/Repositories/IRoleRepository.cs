using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<Role>> GetAllAsync(CancellationToken cancellationToken);
}