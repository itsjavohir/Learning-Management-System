using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class SeasonOverrideConfiguration : IEntityTypeConfiguration<SeasonOverride>
{
    public void Configure(EntityTypeBuilder<SeasonOverride> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Season)
            .HasConversion<int?>();

        builder.Property(item => item.SetAtUtc)
            .IsRequired();

        builder.HasIndex(item => item.SetAtUtc)
            .IsDescending();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(item => item.SetByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}