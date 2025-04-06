using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.enums;
using ClientMeetingHandler.presentation.dto.service;

namespace ClientMeetingHandler.presentation.dto.note;

public class NoteDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType NoteType { get; set; }
    public Guid ServiceId { get; set; }
    public ServiceDto Service { get; set; }
}