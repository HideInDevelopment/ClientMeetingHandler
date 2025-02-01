using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    private readonly ServiceSettings _serviceSettings;

    public ServiceConfiguration(IOptions<AppSettings> appSettings)
    {
        _serviceSettings = appSettings.Value.Service;
    }
    
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(_serviceSettings.NameMaxLength);
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Expiration).IsRequired();
        builder.Property(x => x.ServiceTypeId).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Service_Id");
        builder.HasIndex(x => new { x.Id, x.ServiceTypeId }).HasDatabaseName("IX_ServiceType_Service_Id");
        
        builder.HasOne<ServiceType>().WithOne().HasForeignKey<Service>(x => x.ServiceTypeId);
        builder.HasMany<Note>().WithOne().HasForeignKey(x => x.Id);
    }
}