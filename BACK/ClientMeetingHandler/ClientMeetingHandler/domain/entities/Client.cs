using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto;

namespace ClientMeetingHandler.domain.entities;

public class Client : Entity<Guid>, IMapToDto<ClientDto>
{
    public string Name { get; set; }
    public Guid ContactId { get; set; }
    
    public virtual Contact Contact { get; set; }
    public virtual List<Meeting> Meetings { get; set; }
    public virtual List<Service> Services { get; set; }
    
    public ClientDto ToDto()
    {
        return new ClientDto()
        {
            Id = Id,
            Name = Name,
            ContactId = ContactId
        };
    }
}