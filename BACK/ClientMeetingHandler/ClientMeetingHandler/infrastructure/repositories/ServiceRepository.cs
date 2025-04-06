using System.Linq.Expressions;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly IGenericRepository<Guid, Service> _repository;

    public ServiceRepository(IGenericRepository<Guid, Service> repository)
    {
        _repository = repository;
    }

    public Task<IQueryable<Service>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Service?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Service entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Service entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
    
    public Task<IQueryable<Service>> GetQueryWithIncludesAsync(params string[] includes) => _repository.GetQueryWithIncludesAsync(includes);

    public Task<Service?> GetSingleWithIncludesAsync(Expression<Func<Service, bool>> predicate, params string[] includes) => _repository.GetSingleWithIncludesAsync(predicate, includes);
}