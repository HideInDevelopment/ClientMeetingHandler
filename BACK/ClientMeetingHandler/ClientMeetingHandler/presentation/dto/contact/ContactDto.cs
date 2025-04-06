using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.contact;

public class ContactDto : IDto, IMapToEntity<Contact>
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid ClientId { get; set; }
    
    public Contact ToEntity()
    {
        return new Contact()
        {
            Id = Id,
            Country = Country,
            PhoneNumber = PhoneNumber,
            Email = Email,
            ClientId = ClientId
        };
    }
}