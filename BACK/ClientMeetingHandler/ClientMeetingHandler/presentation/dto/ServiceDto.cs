using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class ServiceDto : IDto, IMapToEntity<Service>
{
    public Service ToEntity()
    {
        throw new NotImplementedException();
    }
}