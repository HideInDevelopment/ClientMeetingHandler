using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.client;

public class ClientDto : IDto, IMapToEntity<Client>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ContactId { get; set; }
    
    
    public Client ToEntity()
    {
        return new Client()
        {
            Id = Id,
            Name = Name,
            ContactId = ContactId,
        };
    }
}