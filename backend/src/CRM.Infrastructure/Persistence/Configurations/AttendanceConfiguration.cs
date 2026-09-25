using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.AbsenceReason)
            .HasMaxLength(500);

        builder.Property(a => a.MentorNote)
            .HasMaxLength(1000);

        builder.Property(a => a.MarkedAt)
            .IsRequired();

        builder.HasOne(a => a.Lesson)
            .WithMany()
            .HasForeignKey(a => a.LessonId);

        builder.HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.MarkedByMentor)
            .WithMany()
            .HasForeignKey(a => a.MarkedByMentorId)
            .OnDelete(DeleteBehavior.Restrict);

        // one attendance record per student per lesson
        builder.HasIndex(a => new { a.LessonId, a.StudentId }).IsUnique();
    }
}
