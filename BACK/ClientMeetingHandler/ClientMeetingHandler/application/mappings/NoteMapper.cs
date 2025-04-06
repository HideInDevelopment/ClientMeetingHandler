using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.note;

namespace ClientMeetingHandler.application.mappings;

public class NoteMapper : GenericMapper<Note, NoteDto, NoteDetailDto>
{
    public override NoteDetailDto? MapDetailEntityToDetailDto(Note? entity)
    {
        if (entity == null) return null;

        return new NoteDetailDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
            NoteType = entity.NoteType,
            ServiceId = entity.ServiceId,
            Service = entity.Service.ToDto(),
        };
    }
}