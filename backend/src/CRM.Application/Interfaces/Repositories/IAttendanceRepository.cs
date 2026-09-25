using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Attendance?> GetByLessonAndStudentAsync(Guid lessonId, Guid studentId, CancellationToken cancellationToken);
    Task<List<Attendance>> GetByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken);
    Task<List<Attendance>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);
    Task AddAsync(Attendance attendance, CancellationToken cancellationToken);
    void Update(Attendance attendance);
    void Delete(Attendance attendance);
}
