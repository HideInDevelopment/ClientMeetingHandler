using ClientMeetingHandler.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ContactId).IsRequired();
        
        builder.HasIndex(x => x.Id).HasDatabaseName("IX_Client_Id");
        builder.HasIndex(x => new { x.Id, x.ContactId }).HasDatabaseName("IX_Contact_Client_Id");
        
        builder.HasOne(c => c.Contact)
            .WithOne(c => c.Client)
            .HasForeignKey<Contact>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.Meetings)
            .WithOne(m => m.Client)
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.Services);

        builder.ToTable("Clients", "ClientMeetingHandler");
    }
}