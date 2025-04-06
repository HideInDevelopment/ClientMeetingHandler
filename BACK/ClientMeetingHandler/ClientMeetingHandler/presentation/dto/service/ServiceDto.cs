using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.service;

public class ServiceDto : IDto, IMapToEntity<Service>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public DateTime Expiration { get; set; }
    public Guid ServiceTypeId { get; set; }
    
    public Service ToEntity()
    {
        return new Service()
        {
            Id = Id,
            Name = Name,
            Date = Date,
            Expiration = Expiration,
            ServiceTypeId = ServiceTypeId,
        };
    }
}