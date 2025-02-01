using ClientMeetingHandler.domain.enums;

namespace ClientMeetingHandler.domain.entities;

public class Note
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType NoteType { get; set; }

    public Note(Guid id, string title, string content, NoteType noteType)
    {
        Id = id;
        Title = title;
        Content = content;
        NoteType = noteType;
    }
}