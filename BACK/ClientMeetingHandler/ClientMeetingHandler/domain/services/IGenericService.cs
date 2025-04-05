namespace ClientMeetingHandler.domain.services;

public interface IGenericService<in TKey, TDto>
{
    // CRUD operations
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(TKey id);
    Task AddAsync(TDto dto);
    Task UpdateAsync(TDto dto);
    Task DeleteAsync(TKey id);
}