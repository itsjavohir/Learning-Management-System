using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class AttendanceRepository(AppDbContext dbcontext) : IAttendanceRepository
{
    public async Task<Attendance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.Attendances
            .Include(a => a.Student).ThenInclude(s => s.User)
            .Include(a => a.MarkedByMentor).ThenInclude(m => m!.User)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Attendance?> GetByLessonAndStudentAsync(Guid lessonId, Guid studentId, CancellationToken cancellationToken)
    {
        return await dbcontext.Attendances
            .FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId, cancellationToken);
    }

    public async Task<List<Attendance>> GetByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        return await dbcontext.Attendances
            .AsNoTracking()
            .Include(a => a.Student).ThenInclude(s => s.User)
            .Include(a => a.MarkedByMentor).ThenInclude(m => m!.User)
            .Where(a => a.LessonId == lessonId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Attendance>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await dbcontext.Attendances
            .AsNoTracking()
            .Include(a => a.Lesson)
            .Where(a => a.StudentId == studentId)
            .OrderByDescending(a => a.MarkedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Attendance attendance, CancellationToken cancellationToken)
    {
        await dbcontext.Attendances.AddAsync(attendance, cancellationToken);
    }

    public void Update(Attendance attendance)
    {
        attendance.MarkAsUpdated();
        dbcontext.Entry(attendance).State = EntityState.Modified;
    }

    public void Delete(Attendance attendance)
    {
        dbcontext.Attendances.Remove(attendance);
    }
}
