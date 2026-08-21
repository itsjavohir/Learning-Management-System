using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class MentorRepository(AppDbContext dbcontext) : IMentorRepository
{
    public async Task AddAsync(Mentor mentor, CancellationToken cancellationToken)
    {
       await dbcontext.Mentors.AddAsync(mentor,cancellationToken);
    }

    public void Delete(Mentor mentor)
    {
        dbcontext.Mentors.Remove(mentor);
    }

    public async Task<List<Mentor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await 
        dbcontext.Mentors.
        AsNoTracking().
        ToListAsync(cancellationToken);
    }

    public void Update(Mentor mentor)
    {
        dbcontext.Mentors.Update(mentor);
        mentor.MarkAsUpdated();
    }

}
