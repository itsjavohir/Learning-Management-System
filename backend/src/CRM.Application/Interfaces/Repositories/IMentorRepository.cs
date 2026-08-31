using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IMentorRepository
{
    Task AddAsync (Mentor mentor,CancellationToken cancellationToken);
    Task<Mentor?>GetByUserIdAsync(Guid Id,CancellationToken cancellationToken);
    void Update (Mentor mentor);
    void Delete (Mentor mentor);
}
