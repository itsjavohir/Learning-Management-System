using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class MentorConfiguration : IEntityTypeConfiguration<Mentor>
{
    public void Configure(EntityTypeBuilder<Mentor> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Specialization)
            .HasMaxLength(200);

        builder.Property(m => m.Bio)
            .HasMaxLength(2000);

    builder.HasOne(m => m.User)
    .WithOne(u => u.Mentor)
    .HasForeignKey<Mentor>(m => m.UserId)
    .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.UserId)
            .IsUnique();
    }
}