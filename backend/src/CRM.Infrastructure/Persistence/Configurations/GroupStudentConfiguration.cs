using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class GroupStudentConfiguration:IEntityTypeConfiguration<GroupStudent>
{
    public void Configure(EntityTypeBuilder<GroupStudent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(gs => gs.RemoveReason)
            .HasMaxLength(500);

        builder.HasOne(gs => gs.Group)
            .WithMany(g => g.GroupStudents)
            .HasForeignKey(gs => gs.GroupId);

        builder.HasOne(gs => gs.Student)
            .WithMany(s => s.GroupStudents)
            .HasForeignKey(gs => gs.StudentId);

        builder.HasOne(gs => gs.TransferredFrom)
            .WithMany()
            .HasForeignKey(gs => gs.TransferredFromGroupStudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(gs => gs.TransferredTo)
            .WithMany()
            .HasForeignKey(gs => gs.TransferredToGroupStudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(gs => new { gs.GroupId, gs.StudentId }).IsUnique();
    }
}
