using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.serviceType;

public class ServiceTypeDto : IDto, IMapToEntity<ServiceType>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }
    
    public ServiceType ToEntity()
    {
        return new ServiceType()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Sessions = Sessions
        };
    }
}