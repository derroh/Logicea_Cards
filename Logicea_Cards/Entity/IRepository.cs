using System.Linq.Expressions;

namespace Logicea_Cards.Entity
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> ReadAllAsync();
        Task<(List<T>, int)> ReadAllFilterAsync(Expression<Func<T, bool>> filter, int skip, int take);
        Task UpdateAsync(T entity, Expression<Func<T, bool>> filter);
        Task<T> ReadAsync(int id);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> ReadAllAsync(Expression<Func<T, bool>> filter);
    }
}
