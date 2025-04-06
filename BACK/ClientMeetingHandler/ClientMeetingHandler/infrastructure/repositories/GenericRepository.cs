using System.Linq.Expressions;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace ClientMeetingHandler.infrastructure.repositories;

public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity>
    where TEntity : Entity<TKey>
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TEntity> _entity;

    public GenericRepository(DatabaseContext context)
    {
        _context = context;
        _entity = context.Set<TEntity>();
    }

    public Task<IQueryable<TEntity>> GetAllAsync()
    {
        var query = _entity.AsSplitQuery();

        return Task.FromResult(query);
    }

    public async Task<TEntity?> GetByIdAsync(TKey id) => await _entity.FindAsync(id);

    public async Task AddAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
        try
        {
            var affectedRows = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new FailOnPersistEntityException<TEntity>(entity);
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _entity.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new FailOnPersistEntityException<TEntity>(entity);
        }
    }
    
    public async Task DeleteAsync(TKey key)
    {
        var entity = await _entity.FindAsync(key);
        if (entity is null) throw new EntityNotFoundException<TEntity>(entity);
        _entity.Remove(entity);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new FailOnPersistEntityException<TEntity>(entity);
        }
    }

    public Task<IQueryable<TEntity>> GetQueryWithIncludesAsync(params string[] includes)
    {
        var query = _entity.AsSplitQuery();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return Task.FromResult(query);
    }

    public async Task<TEntity?> GetSingleWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = await GetQueryWithIncludesAsync(includes);
        return await query.FirstOrDefaultAsync(predicate);
    }
}