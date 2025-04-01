namespace ClientMeetingHandler.domain.repositories;

public interface IGenericRepository<TKey, TEntity> where TEntity : class
{
    // CRUD Operations
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey key);
}