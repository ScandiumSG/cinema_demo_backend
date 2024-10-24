using cinemaServer.Data;
using cinemaServer.Models.PureModels;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace cinemaServer.Repository
{
    public class CurationRepository : Repository<Movie>
    {
        public CurationRepository(DataContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Movie>();
        }

        public async Task<Movie> GetHighlightedMovie() 
        {
            // Sort by rating descending, select the first entry (i.e. highest rated movie)
            return await _dbSet.OrderByDescending(m => m.AverageRating).FirstAsync();
        }
    }
}
