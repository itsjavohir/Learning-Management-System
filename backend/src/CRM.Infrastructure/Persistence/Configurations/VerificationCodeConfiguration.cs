using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.CodeHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.Type)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => new { v.UserId, v.Type, v.IsUsed });
    }
}