using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.meeting;

public class MeetingDto : IDto, IMapToEntity<Meeting>
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocationId { get; set; }
    public Guid ClientId { get; set; }
    
    public Meeting ToEntity()
    {
        return new Meeting
        {
            Id = Id,
            Date = Date,
            Duration = Duration,
            LocationId = LocationId,
            ClientId = ClientId,
        };
    }
}