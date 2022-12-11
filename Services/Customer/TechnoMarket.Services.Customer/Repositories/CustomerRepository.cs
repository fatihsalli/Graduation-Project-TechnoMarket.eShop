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
            //AsNoTracking update edilirken hata almamak için track özelliğini kapatıyoruz.
            var customers = await _context.Customers
                .AsNoTracking()
                .Include(x => x.Address)
                .ToListAsync();

            return customers;
        }

        public async Task<Models.Customer> GetSingleCustomerByIdWithAddressAsync(string customerId)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .Include(x => x.Address)
                .Where(x => x.Id == customerId)
                .SingleOrDefaultAsync();

            return customer;
        }

    }
}
