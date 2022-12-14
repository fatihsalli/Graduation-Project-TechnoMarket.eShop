using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;

namespace TechnoMarket.Services.Catalog.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Başka repositorylerden de ulaşabilmek için protected yaptık
        protected readonly CatalogDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(CatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            //Track edilmesine gerek yok => performans için
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(new Guid(id));
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            //_context.Entry(entity).State= EntityState.Modified; //=>Alternatif 
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
