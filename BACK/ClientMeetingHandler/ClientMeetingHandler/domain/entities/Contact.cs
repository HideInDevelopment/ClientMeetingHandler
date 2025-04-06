using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.contact;

namespace ClientMeetingHandler.domain.entities;

public class Contact : Entity<Guid>, IMapToDto<ContactDto>
{
    public string Country { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual Client Client { get; set; }
    
    public ContactDto ToDto()
    {
        return new ContactDto()
        {
            Id = Id,
            Country = Country,
            PhoneNumber = PhoneNumber,
            Email = Email,
            ClientId = ClientId
        };
    }
}