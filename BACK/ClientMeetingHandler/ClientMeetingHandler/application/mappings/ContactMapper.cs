using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.contact;

namespace ClientMeetingHandler.application.mappings;

public class ContactMapper : GenericMapper<Contact, ContactDto, ContactDetailDto>
{
    public override ContactDetailDto? MapDetailEntityToDetailDto(Contact? entity)
    {
        if(entity == null) return null;

        return new ContactDetailDto
        {
            Id = entity.Id,
            Country = entity.Country,
            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            ClientId = entity.ClientId,
            Client = entity.Client.ToDto(),
        };
    }
}