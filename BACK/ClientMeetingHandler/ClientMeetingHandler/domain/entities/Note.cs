using ClientMeetingHandler.domain.enums;

namespace ClientMeetingHandler.domain.entities;

public class Note : Entity<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType NoteType { get; set; }
    public Guid ServiceId { get; set; }
    
    public virtual Service Service { get; set; }
}