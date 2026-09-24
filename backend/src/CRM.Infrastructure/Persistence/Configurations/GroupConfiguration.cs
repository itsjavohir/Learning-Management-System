using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(g => g.MaxStudents)
            .IsRequired();

        
        builder.HasOne(g => g.Course)
            .WithMany(c => c.Groups)
            .HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

     
        builder.HasOne(g => g.Mentor)
            .WithMany(m => m.Groups)
            .HasForeignKey(g => g.MentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
