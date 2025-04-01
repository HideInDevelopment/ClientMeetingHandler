using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class ContactRepository : IContactRepository
{
    private readonly IGenericRepository<Guid, domain.entities.Contact> _repository;

    public ContactRepository(IGenericRepository<Guid, domain.entities.Contact> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<domain.entities.Contact>> GetAllAsync() => _repository.GetAllAsync();

    public Task<domain.entities.Contact?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(domain.entities.Contact entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(domain.entities.Contact entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);

    public async Task<domain.entities.Contact?> GetByEmail(string email)
    {
        var allContacts = await _repository.GetAllAsync();
        return allContacts.FirstOrDefault(x => x.Email == email);
    }
}