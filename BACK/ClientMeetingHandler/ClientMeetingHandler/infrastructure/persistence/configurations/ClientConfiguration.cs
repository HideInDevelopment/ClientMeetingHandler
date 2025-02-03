using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ClientConfiguration : IEntityConfiguration, IEntityTypeConfiguration<Client>
{
    private readonly ClientSettings _clientSettings;

    public ClientConfiguration(IOptions<AppSettings> appSettings)
    {
        _clientSettings = appSettings.Value.Client;
    }
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }

    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(_clientSettings.NameMaxLength);
        builder.Property(x => x.ContactId).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Client_Id");
        builder.HasIndex(x => new { x.Id, x.ContactId }).HasDatabaseName("IX_Contact_Client_Id");
        
        builder.HasOne<Contact>().WithMany().HasForeignKey(x => x.ContactId);
        builder.HasMany<Meeting>().WithOne().HasForeignKey(x => x.Id);
        builder.HasMany<Service>().WithOne().HasForeignKey(x => x.Id);
    }


}