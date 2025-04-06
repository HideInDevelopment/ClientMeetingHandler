using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.domain.repositories;

public interface IContactRepository : IGenericRepository<Guid, Contact>
{
    
}