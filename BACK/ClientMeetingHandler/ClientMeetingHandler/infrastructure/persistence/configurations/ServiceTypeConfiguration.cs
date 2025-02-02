using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ServiceTypeConfiguration : IEntityConfiguration, IEntityTypeConfiguration<ServiceType>
{
    private readonly ServiceTypeSettings _serviceTypeSettings;

    public ServiceTypeConfiguration(IOptions<AppSettings> appSettings)
    {
        _serviceTypeSettings = appSettings.Value.ServiceType;
    }
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }
    
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(_serviceTypeSettings.NameMaxLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(_serviceTypeSettings.DescriptionMaxLength);
        builder.Property(x => x.Price).IsRequired().HasMaxLength(_serviceTypeSettings.PriceMaxLength);
        builder.Property(x => x.Sessions).IsRequired().HasMaxLength(_serviceTypeSettings.CountryMaxLength);
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_ServiceType_Id");
    }
}