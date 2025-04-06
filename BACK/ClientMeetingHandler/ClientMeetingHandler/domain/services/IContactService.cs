using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.contact;

namespace ClientMeetingHandler.domain.services;

public interface IContactService : IGenericService<Guid, IDto>, IHasEmail<ContactDto, ContactDetailDto>
{
    
}