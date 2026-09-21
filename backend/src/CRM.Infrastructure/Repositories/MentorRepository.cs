using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class MentorRepository(AppDbContext dbcontext) : IMentorRepository
{
    public void Delete(Mentor mentor)
    {
        dbcontext.Mentors.Remove(mentor);
    }

    public async Task<Mentor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbcontext.Mentors
        .Include(m => m.User)
        .FirstOrDefaultAsync(m => m.UserId == userId);
    }

    public async Task<List<Mentor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbcontext.Mentors
        .Include(m => m.User)
        .ToListAsync(cancellationToken);
    }

    public async Task AddAsync (Mentor mentor,CancellationToken cancellationToken)
    {
        await dbcontext.Mentors.AddAsync(mentor,cancellationToken);
    }

    public void Update(Mentor mentor)
    {
       dbcontext.Mentors.Update(mentor);
       mentor.MarkAsUpdated();
    }
    public async Task<Mentor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
{
    return await dbcontext.Mentors
        .Include(m => m.User)
        .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
}
}