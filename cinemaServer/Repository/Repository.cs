using cinemaServer.Data;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DataContext _context;
        protected DbSet<T> _dbSet;

        public Repository(DataContext context) 
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<T?> GetSpecific(object identifier)
        {
            return await _dbSet.FindAsync(identifier);

        }

        /// <inheritdoc />
        public async Task<Tuple<int, T>> Create(T entity)
        {
            _dbSet.Add(entity);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, T>(savedResultEntities, entity);
        }

        /// <inheritdoc />
        public async Task<Tuple<int, List<T>>> CreateMultiple(List<T> entities)
        {
            _dbSet.AddRange(entities);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, List<T>>(savedResultEntities, entities);
        }

        /// <inheritdoc />
        public async Task<T?> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<T?> Delete(object entityId)
        {
            T? foundEntity = await _dbSet.FindAsync(entityId);
            if (foundEntity == null)
            {
                return null;
            }
            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }
    }
}
