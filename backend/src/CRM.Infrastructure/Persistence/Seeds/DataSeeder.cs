using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Persistence.Seeds;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await SeedRolesAsync(context);
        await SeedAdminAsync(context);
    }

    private static async Task SeedRolesAsync(AppDbContext context)
    {
        if (await context.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            new() { Id = Guid.NewGuid(), Name = "Admin" },
            new() { Id = Guid.NewGuid(), Name = "Mentor" },
            new() { Id = Guid.NewGuid(), Name = "Student" }
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAdminAsync(AppDbContext context)
    {
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole is null)
            return; 

        var adminExists = await context.Users.AnyAsync(x => x.RoleId == adminRole.Id);
        if (adminExists)
            return;

        var admin = new User
        {
            FirstName = "Super",
            LastName = "Admin",
            PhoneNumber = "900000000",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            RoleId = adminRole.Id,
            IsActive = true,
            MustChangePassword = true
        };

        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}