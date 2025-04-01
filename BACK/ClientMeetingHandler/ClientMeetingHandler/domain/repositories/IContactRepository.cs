using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories.contracts;

namespace ClientMeetingHandler.domain.repositories;

public interface IContactRepository : IGenericRepository<Guid, Contact>, IHasEmail<Contact>
{
    
}