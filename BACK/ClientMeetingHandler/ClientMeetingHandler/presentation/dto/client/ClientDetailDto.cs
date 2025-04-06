using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.contact;
using ClientMeetingHandler.presentation.dto.meeting;
using ClientMeetingHandler.presentation.dto.service;

namespace ClientMeetingHandler.presentation.dto.client;

public class ClientDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ContactId { get; set; }
    public ContactDto Contact { get; set; }
    public List<MeetingDto> Meetings { get; set; }
    public List<ServiceDto> Services { get; set; }
}