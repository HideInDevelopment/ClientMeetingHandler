using ClientMeetingHandler.application.services;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.infrastructure.persistence;
using ClientMeetingHandler.infrastructure.persistence.configurations;
using ClientMeetingHandler.infrastructure.repositories;
using Microsoft.EntityFrameworkCore;
using Contact = ClientMeetingHandler.domain.entities.Contact;

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
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ILocalizationRepository, LocalizationRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        
        // Services
        services.AddScoped<IClientService, ClientService>();
        
        
        // Fluent API
        services.AddScoped<IEntityTypeConfiguration<Client>, ClientConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Contact>, ContactConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Localization>, LocalizationConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Meeting>, MeetingConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Note>, NoteConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Service>, ServiceConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<ServiceType>, ServiceTypeConfiguration>();
        
        return services;
    }
}