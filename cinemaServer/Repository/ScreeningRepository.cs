using cinemaServer.Data;
using cinemaServer.Models.PureModels;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Repository
{
    public class ScreeningRepository : ICompUpcomingRepository<Screening>
    {
        protected DataContext _context;
        protected DbSet<Screening> _dbSet;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
        public ScreeningRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<Screening>();
        }

        public async Task<Tuple<int, Screening>> Create(Screening entity)
        {
            _dbSet.Add(entity);
            var savedEntities = await _context.SaveChangesAsync();
            return new Tuple<int, Screening>(savedEntities, entity);
        }

        public async Task<Tuple<int, List<Screening>>> CreateMultiple(List<Screening> entities)
        {
            _dbSet.AddRange(entities);
            var savedEntities = await _context.SaveChangesAsync();
            return new Tuple<int, List<Screening>>(savedEntities, entities);
        }

        public async Task<Screening?> Delete(int id1, int id2)
        {
            Screening? foundEntity = await _dbSet.FindAsync(id1, id2);
            if (foundEntity == null)
            {
                return null;
            }
            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }

        public async Task<List<Screening>> Get(int? limit)
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

        public async Task<Screening?> GetSpecific(int id1, int id2)
        {
            return await _dbSet.FindAsync(id1, id2);
        }

        public async Task<Screening?> Update(Screening entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Screening>> GetUpcoming(int limit, DateTime timeCutoff, List<int> locationSpecifier)
        {
            if (locationSpecifier.Count != 0)
            {
                return await _dbSet
                    .Where((e) => 
                        e.StartTime.CompareTo(timeCutoff) > 0 && 
                        locationSpecifier.Contains(e.Theater!.Id))
                    .OrderBy((e) => e.StartTime)
                    .Take(limit)
                    .ToListAsync();
            }
            return await _dbSet
                    .Where((e) => e.StartTime.CompareTo(timeCutoff) > 0)
                    .OrderBy((e) => e.StartTime)
                    .Take(limit)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Screening>> GetSpecificUpcoming(int specificObjectId, int limit, DateTime timeCutoff, List<int> locationSpecifier)
        {
            if (locationSpecifier.Count != 0)
            {
                return await _dbSet
                    .Where((e) => 
                        e.StartTime.CompareTo(timeCutoff) > 0 && 
                        e.MovieId.Equals(specificObjectId) && 
                        locationSpecifier.Contains(e.Theater!.Id))
                    .OrderBy((e) => e.StartTime)
                    .Take(limit)
                    .ToListAsync();
            }

            return await _dbSet
                .Where((e) => e.StartTime.CompareTo(timeCutoff) > 0 && e.MovieId.Equals(specificObjectId))
                .OrderBy((e) => e.StartTime)
                .Take(limit)
                .ToListAsync();
        }
    }
}
