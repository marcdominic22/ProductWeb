using System.Linq.Expressions;

namespace ProductCRUD.Application.Interfaces;

public interface IRepositoryBase<T>
{
    Task<T> CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderBy = null);
    Task<T?> ReadSingle(Expression<Func<T, bool>>? expression = null);
    Task<bool> Find(Expression<Func<T, bool>>? expression = null);
    Task<T> UpdateAsync(T entity);

}
