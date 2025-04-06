using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.note;
using ClientMeetingHandler.presentation.dto.serviceType;

namespace ClientMeetingHandler.presentation.dto.service;

public class ServiceDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public DateTime Expiration { get; set; }
    public Guid ServiceTypeId { get; set; }
    public ServiceTypeDto ServiceType { get; set; }
    public List<NoteDto> Notes { get; set; }
}