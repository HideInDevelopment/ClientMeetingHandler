using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.infrastructure.persistence;
using ClientMeetingHandler.infrastructure.persistence.configurations;
using Microsoft.EntityFrameworkCore;

namespace ClientMeetingHandler.common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //DatabaseContext
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));
        
        // Repositories
        
        // Fluent API
        services.AddScoped<IEntityTypeConfiguration<Client>, ClientConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Contact>, ContactConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Localization>, LocalizationConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Meeting>, MeetingConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Note>, NoteConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Service>, ServiceConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<ServiceType>, ServiceTypeConfiguration>();
        
        // AppSettings
        services.Configure<AppSettings>(configuration.GetSection("Settings"));
        
        return services;
    }
}