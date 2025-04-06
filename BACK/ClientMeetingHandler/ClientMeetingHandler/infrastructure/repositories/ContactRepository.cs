using System.Linq.Expressions;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class ContactRepository : IContactRepository
{
    private readonly IGenericRepository<Guid, Contact> _repository;

    public ContactRepository(IGenericRepository<Guid, Contact> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Contact>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Contact?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Contact entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Contact entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
    
    public Task<IQueryable<Contact>> GetQueryWithIncludesAsync(params string[] includes) => _repository.GetQueryWithIncludesAsync(includes);

    public Task<Contact?> GetSingleWithIncludesAsync(Expression<Func<Contact, bool>> predicate, params string[] includes) => _repository.GetSingleWithIncludesAsync(predicate, includes);

    public async Task<Contact?> GetByEmail(string email)
    {
        var allContacts = await _repository.GetAllAsync();
        return allContacts.FirstOrDefault(x => x.Email == email);
    }
}