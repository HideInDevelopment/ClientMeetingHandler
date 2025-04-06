using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.enums;
using ClientMeetingHandler.presentation.dto.note;

namespace ClientMeetingHandler.domain.entities;

public class Note : Entity<Guid>, IMapToDto<NoteDto>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType NoteType { get; set; }
    public Guid ServiceId { get; set; }
    
    public virtual Service Service { get; set; }
    
    public NoteDto ToDto()
    {
        return new NoteDto
        {
            Id = Id,
            Title = Title,
            Content = Content,
            NoteType = NoteType,
            ServiceId = ServiceId,
        };
    }
}