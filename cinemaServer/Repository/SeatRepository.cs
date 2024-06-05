using cinemaServer.Data;
using cinemaServer.Models.PureModels;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class SeatRepository : IRepository<Seat>
    {
        protected DataContext _context;
        protected DbSet<Seat> _dbSet;

        public SeatRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<Seat>();
        }

        /// <inheritdoc />
        public async Task<List<Seat>> Get(int? limit)
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
        public async Task<Seat?> GetSpecific(object identifier)
        {
            return await _dbSet.FindAsync(identifier);

        }

        /// <inheritdoc />
        public async Task<Tuple<int, Seat>> Create(Seat entity)
        {
            _dbSet.Add(entity);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, Seat>(savedResultEntities, entity);
        }

        /// <inheritdoc />
        public async Task<Tuple<int, List<Seat>>> CreateMultiple(List<Seat> entities)
        {
            _dbSet.AddRange(entities);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, List<Seat>>(savedResultEntities, entities);
        }

        /// <inheritdoc />
        public async Task<Seat?> Update(Seat entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<Seat?> Delete(object entityId)
        {
            Seat? foundEntity = await _dbSet.FindAsync(entityId);
            if (foundEntity == null)
            {
                return null;
            }
            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }

        public async Task<IEnumerable<Seat>> GetSeatsForTheater(int theaterId) 
        {
            return await _dbSet
                .Where((s) => s.TheaterId == theaterId)
                .ToListAsync();
        }
    }
}
