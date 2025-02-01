using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    private readonly MeetingSettings _meetingSettings;

    public MeetingConfiguration(IOptions<AppSettings> appSettings)
    {
        _meetingSettings = appSettings.Value.Meeting;
    }
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Duration).IsRequired().HasMaxLength(_meetingSettings.DurationMaxLength);
        builder.Property(x => x.LocalizationId).IsRequired();
        builder.Property(x => x.ClientId).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Meeting_Id");
        builder.HasIndex(x => new { x.Id, x.ClientId }).HasDatabaseName("IX_Client_Meeting_Id");
        builder.HasIndex(x => new { x.Id, x.LocalizationId }).HasDatabaseName("IX_Localization_Meeting_Id");
        builder.HasIndex(x => new { x.Id, x.ClientId, x.LocalizationId })
            .HasDatabaseName("IX_Client_Localization_Meeting_Id");

        builder.HasOne<Localization>().WithOne().HasForeignKey<Meeting>(x => x.LocalizationId);
    }
}