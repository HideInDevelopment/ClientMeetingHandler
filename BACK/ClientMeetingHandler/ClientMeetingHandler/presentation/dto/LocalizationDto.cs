using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class LocalizationDto : IDto, IMapToEntity<Localization>
{
    public Localization ToEntity()
    {
        throw new NotImplementedException();
    }
}