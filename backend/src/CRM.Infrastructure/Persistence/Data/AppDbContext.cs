using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options ) : DbContext (options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Mentor> Mentors => Set<Mentor>();
    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Student> Students => Set<Student>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
