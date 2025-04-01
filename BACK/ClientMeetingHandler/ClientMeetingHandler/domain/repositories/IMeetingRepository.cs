using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.domain.repositories;

public interface IMeetingRepository : IGenericRepository<Guid, Meeting>
{
    
}