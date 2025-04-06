using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.enums;

namespace ClientMeetingHandler.presentation.dto.note;

public class NoteDto : IDto, IMapToEntity<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType NoteType { get; set; }
    public Guid ServiceId { get; set; }
    
    public Note ToEntity()
    {
        return new Note()
        {
            Id = Id,
            Title = Title,
            Content = Content,
            NoteType = NoteType,
            ServiceId = ServiceId,
        };
    }
}