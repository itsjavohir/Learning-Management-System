using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class ProfileRepository(AppDbContext dbcontext) : IProfileRepository
{
    public async Task<Profile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbcontext.Profiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(Profile profile, CancellationToken cancellationToken)
    {
        await dbcontext.Profiles.AddAsync(profile, cancellationToken);
    }

    public void Update(Profile profile)
    {
        profile.MarkAsUpdated();
        dbcontext.Entry(profile).State = EntityState.Modified;
    }
}
