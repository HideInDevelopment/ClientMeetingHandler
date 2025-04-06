using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.service;

namespace ClientMeetingHandler.domain.entities;

public class Service : Entity<Guid>, IMapToDto<ServiceDto>
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public DateTime Expiration { get; set; }
    public Guid ServiceTypeId { get; set; }
    
    public virtual ServiceType ServiceType { get; set; }
    public virtual List<Note> Notes { get; set; }
    
    public ServiceDto ToDto()
    {
        return new ServiceDto()
        {
            Id = Id,
            Name = Name,
            Date = Date,
            Expiration = Expiration,
            ServiceTypeId = ServiceTypeId,
        };
    }
}