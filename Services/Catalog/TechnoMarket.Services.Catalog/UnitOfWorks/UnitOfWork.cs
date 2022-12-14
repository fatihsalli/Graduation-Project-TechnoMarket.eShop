using AutoMapper;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;

namespace TechnoMarket.Services.Catalog.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;
        public UnitOfWork(CatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
