using System.Linq.Expressions;

namespace ClientMeetingHandler.domain.services;

public interface IGenericService<in TKey, TDto>
{
    // CRUD operations
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(TKey id);
    Task AddAsync(TDto dto);
    Task UpdateAsync(TDto dto);
    Task DeleteAsync(TKey id);
    
    // Functions for dynamic includes
    Task<IEnumerable<TDto?>> GetAllWithIncludesAsync();
    Task<TDto?> GetByIdWithIncludesAsync(TKey id);
}