using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.repositories.contracts;
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
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IContactRepository, ContactRepository>();
        
        // Fluent API
        services.AddScoped<IEntityTypeConfiguration<Client>, ClientConfiguration>();
        services.AddScoped<IEntityConfiguration, ClientConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Contact>, ContactConfiguration>();
        services.AddScoped<IEntityConfiguration, ContactConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Localization>, LocalizationConfiguration>();
        services.AddScoped<IEntityConfiguration, LocalizationConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Meeting>, MeetingConfiguration>();
        services.AddScoped<IEntityConfiguration, MeetingConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Note>, NoteConfiguration>();
        services.AddScoped<IEntityConfiguration, NoteConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Service>, ServiceConfiguration>();
        services.AddScoped<IEntityConfiguration, ServiceConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<ServiceType>, ServiceTypeConfiguration>();
        services.AddScoped<IEntityConfiguration, ServiceTypeConfiguration>();
        
        // AppSettings
        services.Configure<AppSettings>(configuration.GetSection("Settings"));
        
        return services;
    }
}