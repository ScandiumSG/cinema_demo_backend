using cinemaServer.Data;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class CompositeRepository<T> : ICompRepository<T> where T : class
    {
        protected DataContext _context;
        protected DbSet<T> _dbSet;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
        public CompositeRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<Tuple<int, T>> Create(T entity)
        {
            _dbSet.Add(entity);
            var savedEntities = await _context.SaveChangesAsync();
            return new Tuple<int, T>(savedEntities, entity);
        }

        public async Task<Tuple<int, List<T>>> CreateMultiple(List<T> entities)
        {
            _dbSet.AddRange(entities);
            var savedEntities = await _context.SaveChangesAsync();
            return new Tuple<int, List<T>>(savedEntities, entities);
        }

        public async Task<T?> Delete(int id1, int id2)
        {
            T? foundEntity = await _dbSet.FindAsync(id1, id2);
            if (foundEntity == null)
            {
                return null;
            }
            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }

        public async Task<List<T>> Get(int? limit)
        {
            if (limit == null)
            {
                return await _dbSet.ToListAsync();
            }
            else
            {
                return await _dbSet.Take((int)limit).ToListAsync();
            }
        }

        public async Task<T?> GetSpecific(int id1, int id2)
        {
            return await _dbSet.FindAsync(id1, id2);
        }

        public async Task<T?> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
