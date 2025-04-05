using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.infrastructure.persistence.configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClientMeetingHandler.infrastructure.persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Localization> Localizations { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        
        var connectionString = configurationRoot.GetConnectionString("Database");
        
        builder.UseSqlServer(
            connectionString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("__EFMigrationHistory", "ClientMeetingHandler");
            });
        
        return new DatabaseContext(builder.Options);
    }
}