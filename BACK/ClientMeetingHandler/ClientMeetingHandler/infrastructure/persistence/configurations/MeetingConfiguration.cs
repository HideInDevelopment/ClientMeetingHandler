using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Duration).IsRequired().HasMaxLength(10);
        builder.Property(x => x.LocationId).IsRequired();
        builder.Property(x => x.ClientId).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Meeting_Id");
        builder.HasIndex(x => new { x.Id, x.ClientId }).HasDatabaseName("IX_Client_Meeting_Id");
        builder.HasIndex(x => new { x.Id, LocalizationId = x.LocationId }).HasDatabaseName("IX_Localization_Meeting_Id");
        builder.HasIndex(x => new { x.Id, x.ClientId, LocalizationId = x.LocationId })
            .HasDatabaseName("IX_Client_Localization_Meeting_Id");

        builder.HasOne(m => m.Location)
            .WithMany(l => l.Meetings)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.ToTable("Meetings", "ClientMeetingHandler");
    }
}