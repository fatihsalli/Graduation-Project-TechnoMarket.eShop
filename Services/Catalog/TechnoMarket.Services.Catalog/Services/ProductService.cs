using TechnoMarket.Services.Catalog.Data.Interfaces;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService
    {
        private readonly ICatalogContext _context;
        public ProductService(ICatalogContext context)
        {
            _context = context;
        }







    }
}
