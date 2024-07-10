using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.Application.Common.Exceptions;
using ProductCRUD.Application.Interfaces;
using ProductCRUD.Infrastructure.Data;

namespace ProductCRUD.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
{
    private readonly ApplicationDbContext _context;

    private readonly CancellationToken cancellationToken = new CancellationToken();

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Select All Records
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderBy = null)
    {
        return await DoReadAll(expression,orderBy);
    }

    /// <summary>
    /// Read Single Entity
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includeExpressions"></param>
    /// <returns></returns>
    public async Task<T?> ReadSingle(Expression<Func<T, bool>>? expression = null)
    {
        return await DoReadSingle(expression);
    }

    private async Task<IEnumerable<T>> DoReadAll(Expression<Func<T, bool>>? expression = null,Expression<Func<T, object>>? orderBy = null)
    {
        try
        {
            IQueryable<T> source = _context.Set<T>();

             if (expression != null)
            {
                source = source.Where(expression);
            }

            // Apply ordering if orderBy expression is provided
            if (orderBy != null)
            {
                source = source.OrderBy(orderBy);
            }

            return await source.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to read entity of {typeof(T).Name}", ex);
        }
    }

    public async Task<bool> Find(Expression<Func<T, bool>>? expression = null)
    {
        try
        {
            IQueryable<T> source = _context.Set<T>();

            if (expression != null)
            {
                source = source.Where(expression);
            }

            return await source.AsNoTracking().FirstOrDefaultAsync() != null;
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to read entity of {typeof(T).Name}", ex);
        }
    }

    private async Task<T?> DoReadSingle(Expression<Func<T, bool>>? expression = null)
    {
        try
        {
            IQueryable<T> source = _context.Set<T>();

            if (expression != null)
            {
                source = source.Where(expression);
            }

            return await source.AsNoTracking().FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to read entity of {typeof(T).Name}", ex);
        }
    }

    /// <summary>
    /// Create Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to create entity of {typeof(T).Name}", ex);
        }
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to update entity of {typeof(T).Name}", ex);
        }
    }

    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task DeleteAsync(T entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Failed to delete entity of {typeof(T).Name}", ex);
        }
    }

}
