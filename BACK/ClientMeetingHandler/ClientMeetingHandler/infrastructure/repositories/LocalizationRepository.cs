using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.repositories;

namespace ClientMeetingHandler.infrastructure.repositories;

public class LocalizationRepository: ILocalizationRepository
{
    private readonly IGenericRepository<Guid, Localization> _repository;

    public LocalizationRepository(IGenericRepository<Guid, Localization> repository)
    {
        _repository = repository;
    }
    
    public Task<IQueryable<Localization>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Localization?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Localization entity) => _repository.AddAsync(entity);

    public Task UpdateAsync(Localization entity) => _repository.UpdateAsync(entity);

    public Task DeleteAsync(Guid key) => _repository.DeleteAsync(key);
}