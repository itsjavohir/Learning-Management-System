using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IMentorRepository
{
    void Delete(Mentor mentor);
    Task<Mentor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Mentor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Mentor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Mentor mentor, CancellationToken cancellationToken);
    void Update(Mentor mentor);
}