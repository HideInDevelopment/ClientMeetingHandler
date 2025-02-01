using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    private readonly ContactSettings _contactSettings;

    public ContactConfiguration(IOptions<AppSettings> appSettings)
    {
        _contactSettings = appSettings.Value.Contact;
    }
    
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Country).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Email).IsRequired();
    }
}