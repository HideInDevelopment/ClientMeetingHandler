using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.enums;
using ClientMeetingHandler.presentation.dto;

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
        throw new NotImplementedException();
    }
}