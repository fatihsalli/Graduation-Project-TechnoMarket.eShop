using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;

namespace TechnoMarket.Services.Customer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            await _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
