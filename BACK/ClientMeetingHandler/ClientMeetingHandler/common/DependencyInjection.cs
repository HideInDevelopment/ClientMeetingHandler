using ClientMeetingHandler.application.mappings;
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
            options.UseSqlServer(configuration.GetConnectionString("ClientMeetingHandler")));
        
        // Repositories
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        
        // Mappers
        services.AddScoped<ClientMapper>();
        services.AddScoped<ContactMapper>();
        services.AddScoped<LocationMapper>();
        services.AddScoped<MeetingMapper>();
        services.AddScoped<NoteMapper>();
        services.AddScoped<ServiceMapper>();
        services.AddScoped<ServiceTypeMapper>();
        
        // Services
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IContactService, ContactService>();
        
        // Fluent API
        services.AddScoped<IEntityTypeConfiguration<Client>, ClientConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Contact>, ContactConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Location>, LocationConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Meeting>, MeetingConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Note>, NoteConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<Service>, ServiceConfiguration>();
        services.AddScoped<IEntityTypeConfiguration<ServiceType>, ServiceTypeConfiguration>();
        
        return services;
    }
}