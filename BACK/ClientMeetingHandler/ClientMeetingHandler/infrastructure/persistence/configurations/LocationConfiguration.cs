using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }
    
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Country).IsRequired().HasMaxLength(100);
        builder.Property(x => x.City).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(250);
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Location_Id");
    }
}