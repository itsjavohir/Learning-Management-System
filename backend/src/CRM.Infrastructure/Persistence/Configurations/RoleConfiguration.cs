using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(r => r.Name)
            .IsUnique();

      builder.HasData(
    new Role
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Name = "Admin",
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    },
    new Role
    {
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        Name = "Mentor",
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    },
    new Role
    {
        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
        Name = "Student",
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    }
);
    }
}