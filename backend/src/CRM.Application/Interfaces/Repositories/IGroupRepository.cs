using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<List<Group>> GetAllAsync(CancellationToken cancellationToken);
    Task<Group?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Group group, CancellationToken cancellationToken);
    void Update(Group group);
    void Delete(Group group);
}