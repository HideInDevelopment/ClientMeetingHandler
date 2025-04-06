using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.meeting;

namespace ClientMeetingHandler.domain.entities;

public class Meeting : Entity<Guid>, IMapToDto<MeetingDto>
{
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocationId { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual Location Location { get; set; }
    public virtual Client Client { get; set; }
    
    public MeetingDto ToDto()
    {
        return new MeetingDto
        {
            Id = Id,
            Date = Date,
            Duration = Duration,
            LocationId = LocationId,
            ClientId = ClientId,
        };
    }
}