using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class LocationRepository: ILocationRepository
{
    private readonly IGenericRepository<Guid, Location> _repository;

    public LocationRepository(IGenericRepository<Guid, Location> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Location>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Location?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Location entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Location entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
}