using Microsoft.EntityFrameworkCore;
using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.Repositories.Interfaces;

namespace TechnoMarket.Services.Customer.Repositories
{
    public class CustomerRepository : GenericRepository<Models.Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {

        }

        public async Task<List<Models.Customer>> GetCustomersWithAddressAsync()
        {
            //Eager Loading
            //AsNoTracking update edilirken hata almamak için
            var customers=await _context.Customers.AsNoTracking().Include(x => x.Address).ToListAsync();
            return customers;
        }
    }
}
