using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.meeting;

namespace ClientMeetingHandler.presentation.dto.location;

public class LocationDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public List<MeetingDto> Meetings { get; set; }
}