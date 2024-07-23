using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Database.Configurations;

internal sealed class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.ToTable("Reminders");

        builder.HasKey(r => r.ReminderId);

        builder.Property(r => r.ReminderId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.Property(r => r.ReminderTime)
            .IsRequired();

        builder.Property(r => r.Email)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false);

        builder.Property(r => r.IsSent)
            .IsRequired();
    }
}
