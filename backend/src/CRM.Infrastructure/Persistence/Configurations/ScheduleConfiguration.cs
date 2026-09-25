using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.DayOfWeek)
            .IsRequired();

        builder.Property(s => s.StartTime)
            .IsRequired();

        builder.Property(s => s.EndTime)
            .IsRequired();

        builder.Property(s => s.Room)
            .HasMaxLength(100);

        builder.Property(s => s.RecurringFrom)
            .IsRequired();

        builder.HasOne(s => s.Group)
            .WithMany()
            .HasForeignKey(s => s.GroupId);

        builder.HasIndex(s => new { s.GroupId, s.DayOfWeek });
    }
}
