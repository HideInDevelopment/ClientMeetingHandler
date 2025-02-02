using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.domain.repositories.contracts;

public interface IContactRepository : IGenericRepository<Guid, Contact>, IHasEmailRepository<Contact>
{
    
}