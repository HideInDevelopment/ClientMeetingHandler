using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    private readonly NoteSettings _noteSettings;
    
    public NoteConfiguration(IOptions<AppSettings> appSettings)
    {
        _noteSettings = appSettings.Value.Note;
    }
    
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title).IsRequired().HasMaxLength(_noteSettings.TitleMaxLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(_noteSettings.ContentMaxLength);
        builder.Property(x => x.NoteType).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Note_Id");
    }
}