using Microsoft.EntityFrameworkCore;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;

namespace TechnoMarket.Services.Catalog.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext context) : base(context)
        {


        }

        //Category ve ProductFeature classlarını doldurmak için
        public async Task<List<Product>> GetProductsWithCategoryAndFeaturesAsync()
        {
            //Eager Loading
            //AsNoTracking update edilirken hata almamak için track özelliğini kapatıyoruz.
            var products = await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Feature)
                .ToListAsync();

            return products;
        }

        //Not: Bu metot örnek olması için tutuldu. Owned type yöntemi ile eklendiği için getall yapıldığında address kısmını da dolduruyor.
        public async Task<Product> GetSingleProductByIdWithCategoryAndFeaturesAsync(string productId)
        {
            var customer = await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Feature)
                .Where(x => x.Id == new Guid(productId))
                .SingleOrDefaultAsync();

            return customer;
        }


    }
}
