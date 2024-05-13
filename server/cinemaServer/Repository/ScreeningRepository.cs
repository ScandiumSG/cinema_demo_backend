using cinemaServer.Models.PureModels;

namespace cinemaServer.Repository
{
    public class ScreeningRepository : IRepository<Screening>
    {
        public Task<Screening?> Create(Screening entity)
        {
            throw new NotImplementedException();
        }

        public Task<Screening?> Delete(Screening entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Screening>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Screening?> GetSpecific(object identifier)
        {
            throw new NotImplementedException();
        }

        public Task<Screening?> Update(Screening entity)
        {
            throw new NotImplementedException();
        }
    }
}
