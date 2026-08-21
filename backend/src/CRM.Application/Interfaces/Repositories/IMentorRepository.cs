using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IMentorRepository
{
    Task AddAsync (Mentor mentor,CancellationToken cancellationToken);
    Task<List<Mentor>> GetAllAsync(CancellationToken cancellationToken);

    void Update (Mentor mentor);
    void Delete (Mentor mentor);
}
