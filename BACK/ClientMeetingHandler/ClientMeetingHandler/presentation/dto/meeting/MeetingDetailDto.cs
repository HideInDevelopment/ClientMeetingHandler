using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.client;
using ClientMeetingHandler.presentation.dto.location;

namespace ClientMeetingHandler.presentation.dto.meeting;

public class MeetingDetailDto : IDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocationId { get; set; }
    public Guid ClientId { get; set; }
    public LocationDto Location { get; set; }
    public ClientDto Client { get; set; }
}