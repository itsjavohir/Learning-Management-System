using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class LessonRepository(AppDbContext dbcontext) : ILessonRepository
{
    public async Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.Lessons
            .Include(l => l.Group)
                .ThenInclude(g => g.Course)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<Lesson>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return await dbcontext.Lessons
            .AsNoTracking()
            .Include(l => l.Group)
            .Where(l => l.GroupId == groupId)
            .OrderBy(l => l.WeekNumber)
            .ThenBy(l => l.LessonDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        await dbcontext.Lessons.AddAsync(lesson, cancellationToken);
    }

    public void Update(Lesson lesson)
    {
        lesson.MarkAsUpdated();
        dbcontext.Entry(lesson).State = EntityState.Modified;
    }

    public void Delete(Lesson lesson)
    {
        dbcontext.Lessons.Remove(lesson);
    }
}
