using ClientMeetingHandler.infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace ClientMeetingHandler.domain.repositories;

public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity>
    where TEntity : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TEntity> _entity;

    public GenericRepository(DatabaseContext context)
    {
        _context = context;
        _entity = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _entity.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _entity.FindAsync(id) ?? throw new KeyNotFoundException();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _entity.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(TEntity entity)
    {
        _entity.Remove(entity);
        await _context.SaveChangesAsync();
    }
}