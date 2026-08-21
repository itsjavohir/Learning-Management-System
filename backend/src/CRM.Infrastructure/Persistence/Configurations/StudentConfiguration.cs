using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Balance)
            .HasColumnType("decimal(18,2)");

       builder.HasOne(s => s.User)
    .WithOne(u => u.Student)
    .HasForeignKey<Student>(s => s.UserId)
    .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.UserId)
            .IsUnique();
    }
}