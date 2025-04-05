using System.Runtime.CompilerServices;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto;

namespace ClientMeetingHandler.domain.entities;

public class ServiceType : Entity<Guid>, IMapToDto<ServiceTypeDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }
    
    public ServiceTypeDto ToDto()
    {
        throw new NotImplementedException();
    }
}