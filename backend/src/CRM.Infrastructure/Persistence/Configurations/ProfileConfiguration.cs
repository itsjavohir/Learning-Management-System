using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .HasMaxLength(100);

        builder.Property(p => p.AvatarUrl)
            .HasMaxLength(500);

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .HasMaxLength(300);

        builder.Property(p => p.TelegramUsername)
            .HasMaxLength(64);

        builder.Property(p => p.LinkedInUrl)
            .HasMaxLength(500);

        builder.Property(p => p.GithubUrl)
            .HasMaxLength(500);

        builder.Property(p => p.AboutMe)
            .HasMaxLength(2000);

        builder.HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.UserId)
            .IsUnique();
    }
}
