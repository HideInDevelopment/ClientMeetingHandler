using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class LocalizationConfiguration : IEntityConfiguration, IEntityTypeConfiguration<Localization>
{
    private readonly LocalizationSettings _localizationSettings;

    public LocalizationConfiguration(IOptions<AppSettings> appSettings)
    {
        _localizationSettings = appSettings.Value.Localization;
    }
    
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }
    
    public void Configure(EntityTypeBuilder<Localization> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Country).IsRequired().HasMaxLength(_localizationSettings.CountryMaxLength);
        builder.Property(x => x.City).IsRequired().HasMaxLength(_localizationSettings.CityMaxLength);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(_localizationSettings.StreetMaxLength);
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Localization_Id");
    }
}