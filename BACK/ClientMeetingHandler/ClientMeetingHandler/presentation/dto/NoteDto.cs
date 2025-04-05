using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class NoteDto : IDto, IMapToEntity<Note>
{
    public Note ToEntity()
    {
        throw new NotImplementedException();
    }
}