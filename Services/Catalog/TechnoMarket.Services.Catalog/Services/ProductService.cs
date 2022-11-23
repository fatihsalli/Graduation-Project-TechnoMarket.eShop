using AutoMapper;
using TechnoMarket.Services.Catalog.Data.Interfaces;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public ProductService(ICatalogContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }










    }
}
