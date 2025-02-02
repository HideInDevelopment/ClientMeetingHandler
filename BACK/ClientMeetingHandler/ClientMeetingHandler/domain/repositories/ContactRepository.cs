using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories.contracts;
using ClientMeetingHandler.infrastructure.persistence;

namespace ClientMeetingHandler.domain.repositories;

public class ContactRepository : GenericRepository<Guid, Contact>, IContactRepository
{
    public ContactRepository(DatabaseContext context) : base(context)
    {
    }

    public Task<Contact?> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }
}