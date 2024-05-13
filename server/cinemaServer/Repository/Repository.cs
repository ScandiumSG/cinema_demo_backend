
namespace cinemaServer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Task<T?> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetSpecific(object identifier)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
