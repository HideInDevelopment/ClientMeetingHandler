using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.serviceType;

namespace ClientMeetingHandler.domain.entities;

public class ServiceType : Entity<Guid>, IMapToDto<ServiceTypeDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }
    
    public ServiceTypeDto ToDto()
    {
        return new ServiceTypeDto()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Sessions = Sessions,
        };
    }
}