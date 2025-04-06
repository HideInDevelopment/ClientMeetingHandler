using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.client;

namespace ClientMeetingHandler.application.mappings;

public class ClientMapper : GenericMapper<Client, ClientDto, ClientDetailDto>
{
    public override ClientDetailDto? MapDetailEntityToDetailDto(Client? entity)
    {
        if(entity == null) return null;
    
        return new ClientDetailDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ContactId = entity.ContactId,
            Contact = entity.Contact.ToDto(),
            Meetings = entity.Meetings.Select(x => x.ToDto()).ToList(),
            Services = entity.Services.Select(x => x.ToDto()).ToList(),
        };
    }
}