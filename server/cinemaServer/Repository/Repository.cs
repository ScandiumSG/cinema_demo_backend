
using cinemaServer.Data;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _context;
        private DbSet<T> _dbSet;

        public Repository(DataContext context) 
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <inheritdoc />
        public async Task<List<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<T?> GetSpecific(object identifier)
        {
            return await _dbSet.FindAsync(identifier);

        }

        /// <inheritdoc />
        public async Task<T?> Create(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
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
