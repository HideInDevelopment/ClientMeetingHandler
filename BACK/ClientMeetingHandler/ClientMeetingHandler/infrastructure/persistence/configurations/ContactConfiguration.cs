using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ContactConfiguration : IEntityConfiguration, IEntityTypeConfiguration<Contact>
{
    private readonly ContactSettings _contactSettings;

    public ContactConfiguration(IOptions<AppSettings> appSettings)
    {
        _contactSettings = appSettings.Value.Contact;
    }
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }
    
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Country).IsRequired().HasMaxLength(_contactSettings.CountryMaxLength);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(_contactSettings.PhoneNumberMaxLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(_contactSettings.EmailMaxLength);
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Contact_Id");
        builder.HasIndex(x => x.Email).HasDatabaseName("IX_Contact_Email");
    }
}