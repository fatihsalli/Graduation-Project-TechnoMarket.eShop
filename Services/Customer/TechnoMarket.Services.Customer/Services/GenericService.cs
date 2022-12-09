using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Services.Interfaces;

namespace TechnoMarket.Services.Customer.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {



        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsycn(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
