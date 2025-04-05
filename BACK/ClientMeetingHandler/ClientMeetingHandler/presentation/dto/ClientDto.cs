using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class ClientDto : IDto, IMapToEntity<Client>
{
    public Client ToEntity()
    {
        throw new NotImplementedException();
    }
}