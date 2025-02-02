using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.infrastructure.persistence;

namespace ClientMeetingHandler.domain.repositories;

public class ContactRepository: GenericRepository<Guid, Contact>
{
    public ContactRepository(DatabaseContext context) : base(context)
    {
    }
}