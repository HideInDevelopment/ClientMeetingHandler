using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class NoteRepository : INoteRepository
{
    private readonly IGenericRepository<Guid, Note> _repository;

    public NoteRepository(IGenericRepository<Guid, Note> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Note>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Note?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Note entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Note entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
}