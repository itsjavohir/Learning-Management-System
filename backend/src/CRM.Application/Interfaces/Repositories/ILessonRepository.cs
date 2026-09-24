using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface ILessonRepository
{
    Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Lesson>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task AddAsync(Lesson lesson, CancellationToken cancellationToken);
    void Update(Lesson lesson);
    void Delete(Lesson lesson);
}
