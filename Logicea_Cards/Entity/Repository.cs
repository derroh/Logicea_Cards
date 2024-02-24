using Logicea_Cards.Exceptions;
using Logicea_Cards.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logicea_Cards.Entity
{
    public class Repository<T> : IRepository<T> where T : class
    {
        CardsDbContext _context;
        public Repository(CardsDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ReadAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<(List<T>, int)> ReadAllFilterAsync(int skip, int take)
        {
            var all = _context.Set<T>();
            var relevant = await all.Skip(skip).Take(take).ToListAsync();
            var total = all.Count();

            (List<T>, int) result = (relevant, total);

            return result;
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<T> ReadAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await _context.Set<T>().Where(filter).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException("Record not found.");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> ReadAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }
    }
}
