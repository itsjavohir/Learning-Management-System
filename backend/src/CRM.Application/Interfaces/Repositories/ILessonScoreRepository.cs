using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface ILessonScoreRepository
{
    Task<LessonScore?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<LessonScore?> GetByLessonAndStudentAsync(Guid lessonId, Guid studentId, CancellationToken cancellationToken);
    Task<List<LessonScore>> GetByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken);
    Task<List<LessonScore>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);
    Task AddAsync(LessonScore lessonScore, CancellationToken cancellationToken);
    void Update(LessonScore lessonScore);
    void Delete(LessonScore lessonScore);
}
