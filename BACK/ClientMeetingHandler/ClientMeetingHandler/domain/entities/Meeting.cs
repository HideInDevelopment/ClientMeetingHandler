using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto;

namespace ClientMeetingHandler.domain.entities;

public class Meeting : Entity<Guid>, IMapToDto<MeetingDto>
{
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocalizationId { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual Localization Localization { get; set; }
    public virtual Client Client { get; set; }
    
    public MeetingDto ToDto()
    {
        return new MeetingDto()
        {
            Id = Id,
            Date = Date,
            Duration = Duration,
            LocalizationId = LocalizationId,
            ClientId = ClientId,
        };
    }
}