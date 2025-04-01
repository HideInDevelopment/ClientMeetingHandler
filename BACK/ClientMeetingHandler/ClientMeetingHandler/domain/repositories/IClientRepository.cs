using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.domain.repositories;

public interface IClientRepository: IGenericRepository<Guid, Client>
{
    
}