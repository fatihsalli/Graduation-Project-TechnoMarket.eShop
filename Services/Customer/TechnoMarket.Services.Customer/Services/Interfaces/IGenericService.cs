using System.Linq.Expressions;

namespace TechnoMarket.Services.Customer.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsycn(IEnumerable<T> entities);
    }
}
