using Castle.Core.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Services.Catalog.Controllers;
using TechnoMarket.Services.Catalog.Services.Interfaces;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class ProductsControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _productsController;

        public ProductsControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _productsController = new ProductsController(_mockProductService.Object);
        }



    }
}
