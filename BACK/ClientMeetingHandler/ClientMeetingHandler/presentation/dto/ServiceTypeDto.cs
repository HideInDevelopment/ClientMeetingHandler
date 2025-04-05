using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class ServiceTypeDto : IDto, IMapToEntity<ServiceType>
{
    public ServiceType ToEntity()
    {
        throw new NotImplementedException();
    }
}