using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class GroupStudentConfiguration:IEntityTypeConfiguration<GroupStudent>
{
    public void Configure(EntityTypeBuilder<GroupStudent> builder)
{
    builder.HasKey(x => x.Id);

    builder.HasOne(gs => gs.Group)
        .WithMany(g => g.GroupStudents)
        .HasForeignKey(gs => gs.GroupId);

    builder.HasOne(gs => gs.Student)
        .WithMany()
        .HasForeignKey(gs => gs.StudentId);

    builder.HasIndex(gs => new { gs.GroupId, gs.StudentId }).IsUnique();
}
}

