using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dtos;

public class ContactDto : IDto, IMapToEntity<Contact>
{
    public Contact ToEntity()
    {
        throw new NotImplementedException();
    }
}