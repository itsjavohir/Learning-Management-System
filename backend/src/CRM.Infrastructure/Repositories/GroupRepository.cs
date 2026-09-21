using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class GroupRepository(AppDbContext dbcontext) : IGroupRepository
{
    public async Task<List<Group>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbcontext.Groups
            .AsNoTracking()
            .Include(g => g.Course)
            .Include(g => g.Mentor)
                .ThenInclude(m => m.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<Group?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.Groups
            .Include(g => g.Course)
            .Include(g => g.Mentor)
                .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task AddAsync(Group group, CancellationToken cancellationToken)
    {
        await dbcontext.Groups.AddAsync(group, cancellationToken);
    }

    public void Update(Group group)
    {
        dbcontext.Groups.Update(group);
        group.MarkAsUpdated();
    }

    public void Delete(Group group)
    {
        dbcontext.Groups.Remove(group);
    }
}