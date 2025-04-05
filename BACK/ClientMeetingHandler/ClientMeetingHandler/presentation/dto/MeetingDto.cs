using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class MeetingDto : IDto, IMapToEntity<Meeting>
{
    public Meeting ToEntity()
    {
        throw new NotImplementedException();
    }
}