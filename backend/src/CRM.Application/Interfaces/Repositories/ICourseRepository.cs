using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync(CancellationToken cancellationToken);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Course course, CancellationToken cancellationToken);
    void Update(Course course);
    void Delete(Course course);
}
