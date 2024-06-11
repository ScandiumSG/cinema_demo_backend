using cinemaServer.Data;
using cinemaServer.Models.PureModels;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class TicketRepository : IRepository<Ticket>
    {
        protected DataContext _context;
        protected DbSet<Ticket> _dbSet;

        public TicketRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<Ticket>();
        }

        /// <inheritdoc />
        public async Task<List<Ticket>> Get(int? limit)
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
        public async Task<Ticket?> GetSpecific(object identifier)
        {
            return await _dbSet.FindAsync(identifier);

        }


        public async Task<List<Ticket>> GetTicketsForScreening(int screeningId, int movieId) 
        {
            return await _dbSet
                .Where((t) => t.MovieId == movieId && t.ScreeningId == screeningId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Tuple<int, Ticket>> Create(Ticket entity)
        {
            _dbSet.Add(entity);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, Ticket>(savedResultEntities, entity);
        }

        /// <inheritdoc />
        public async Task<Tuple<int, List<Ticket>>> CreateMultiple(List<Ticket> entities)
        {
            _dbSet.AddRange(entities);
            int savedResultEntities = await _context.SaveChangesAsync();
            return new Tuple<int, List<Ticket>>(savedResultEntities, entities);
        }

        /// <inheritdoc />
        public async Task<Ticket?> Update(Ticket entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<Ticket?> Delete(object entityId)
        {
            Ticket? foundEntity = await _dbSet.FindAsync(entityId);
            if (foundEntity == null)
            {
                return null;
            }
            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }

        public async Task<List<Ticket>> GetTicketsForUser(string userId) 
        {
            List<Ticket> tickets = await _dbSet.Where((t) => t.CustomerId == userId)
                .Include(t => t.Screening)
                .ToListAsync();
            return tickets;
        }
    }
}
