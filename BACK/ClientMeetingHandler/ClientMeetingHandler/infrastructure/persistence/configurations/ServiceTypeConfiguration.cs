using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }
    
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Price).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Sessions).IsRequired().HasMaxLength(10);
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_ServiceType_Id");

        builder.HasMany<Service>()
            .WithOne(s => s.ServiceType)
            .HasForeignKey(s => s.ServiceTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.ToTable("ServiceTypes", "ClientMeetingHandler");
    }
}