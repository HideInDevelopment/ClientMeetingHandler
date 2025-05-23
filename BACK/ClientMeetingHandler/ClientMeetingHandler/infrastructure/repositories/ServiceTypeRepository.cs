using System.Linq.Expressions;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class ServiceTypeRepository : IServiceTypeRepository
{
    private readonly IGenericRepository<Guid, ServiceType> _repository;

    public ServiceTypeRepository(IGenericRepository<Guid, ServiceType> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<ServiceType>> GetAllAsync() => _repository.GetAllAsync();

    public Task<ServiceType?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(ServiceType entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(ServiceType entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
    
    public Task<IQueryable<ServiceType>> GetQueryWithIncludesAsync(IEnumerable<string> includes) => _repository.GetQueryWithIncludesAsync(includes);

    public Task<ServiceType?> GetSingleWithIncludesAsync(Expression<Func<ServiceType, bool>> predicate, IEnumerable<string> includes) => _repository.GetSingleWithIncludesAsync(predicate, includes);
}