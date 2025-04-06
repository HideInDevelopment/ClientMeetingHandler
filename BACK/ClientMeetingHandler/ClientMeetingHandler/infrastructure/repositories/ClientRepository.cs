using System.Linq.Expressions;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class ClientRepository : IClientRepository
{
    private readonly IGenericRepository<Guid, Client> _repository;

    public ClientRepository(IGenericRepository<Guid, Client> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Client>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Client?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Client entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Client entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
    public Task<IQueryable<Client>> GetQueryWithIncludesAsync(params string[] includes) => _repository.GetQueryWithIncludesAsync(includes);

    public Task<Client?> GetSingleWithIncludesAsync(Expression<Func<Client, bool>> predicate, params string[] includes) => _repository.GetSingleWithIncludesAsync(predicate, includes);
}