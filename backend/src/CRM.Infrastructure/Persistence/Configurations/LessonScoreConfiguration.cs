using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class LessonScoreConfiguration : IEntityTypeConfiguration<LessonScore>
{
    public void Configure(EntityTypeBuilder<LessonScore> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Score)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(s => s.MentorFeedback)
            .HasMaxLength(1000);

        builder.Property(s => s.ScoredAt)
            .IsRequired();

        builder.HasOne(s => s.Lesson)
            .WithMany()
            .HasForeignKey(s => s.LessonId);

        builder.HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.ScoredByMentor)
            .WithMany()
            .HasForeignKey(s => s.ScoredByMentorId)
            .OnDelete(DeleteBehavior.Restrict);

        // one score per student per lesson
        builder.HasIndex(s => new { s.LessonId, s.StudentId }).IsUnique();
    }
}
