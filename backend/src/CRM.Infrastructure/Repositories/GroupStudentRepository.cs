using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class GroupStudentRepository(AppDbContext dbcontext) : IGroupStudentRepository
{
    public async Task<GroupStudent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.GroupStudents
            .Include(gs => gs.Group)
            .Include(gs => gs.Student)
                .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(gs => gs.Id == id, cancellationToken);
    }

    public async Task<GroupStudent?> GetByGroupAndStudentAsync(Guid groupId, Guid studentId, CancellationToken cancellationToken)
    {
        return await dbcontext.GroupStudents
            .Include(gs => gs.Group)
            .Include(gs => gs.Student)
                .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(gs => gs.GroupId == groupId && gs.StudentId == studentId, cancellationToken);
    }

    public async Task<List<GroupStudent>> GetByGroupIdAsync(Guid groupId, bool includeInactive, CancellationToken cancellationToken)
    {
        return await dbcontext.GroupStudents
            .AsNoTracking()
            .Include(gs => gs.Group)
            .Include(gs => gs.Student)
                .ThenInclude(s => s.User)
            .Where(gs => gs.GroupId == groupId && (includeInactive || gs.IsActive))
            .OrderBy(gs => gs.JoinedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<GroupStudent>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await dbcontext.GroupStudents
            .AsNoTracking()
            .Include(gs => gs.Group)
            .Include(gs => gs.Student)
                .ThenInclude(s => s.User)
            .Where(gs => gs.StudentId == studentId)
            .OrderByDescending(gs => gs.JoinedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountActiveByGroupIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return await dbcontext.GroupStudents
            .CountAsync(gs => gs.GroupId == groupId && gs.IsActive, cancellationToken);
    }

    public async Task AddAsync(GroupStudent groupStudent, CancellationToken cancellationToken)
    {
        await dbcontext.GroupStudents.AddAsync(groupStudent, cancellationToken);
    }

    public void Update(GroupStudent groupStudent)
    {
        groupStudent.MarkAsUpdated();
        dbcontext.Entry(groupStudent).State = EntityState.Modified;
    }

    public void Delete(GroupStudent groupStudent)
    {
        dbcontext.GroupStudents.Remove(groupStudent);
    }
}
