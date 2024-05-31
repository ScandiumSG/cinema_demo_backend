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

        public Task<Ticket?> Create(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> Delete(object entityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> Get(int? limit)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> GetSpecific(object identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ticket>> GetSpecificByScreening(int screeningId) 
        {
            return await _dbSet.Where((t) => t.ScreeningId == screeningId).ToListAsync();
        }

        public Task<Ticket?> Update(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
