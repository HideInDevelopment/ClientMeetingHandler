using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class MeetingRepository: IMeetingRepository
{
    private readonly IGenericRepository<Guid, Meeting> _repository;

    public MeetingRepository(IGenericRepository<Guid, Meeting> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Meeting>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Meeting?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Meeting entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Meeting entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
}