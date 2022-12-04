using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Services.Catalog.Controllers;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Exceptions;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class ProductsControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _productsController;

        //Fake data
        private List<ProductDto> _products;

        public ProductsControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _productsController = new ProductsController(_mockProductService.Object);

            _products = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id="507f191e810c19729de860ea",
                    Name="Iphone X",
                    Stock=10,
                    Price=950.00M,
                    Description="Super Retina HD display and A11 Bionic chip with 64-bit architecture",
                    ProductFeature="Color=black Height=5.65 inches Width=2.79 inches 0.6 kg"
                },
                new ProductDto()
                {
                    Id="511f191e810c19729de860fr",
                    Name="Asus Zenbook Pro Duo 15",
                    Stock=6,
                    Price=3500.00M,
                    Description="ZenBook Pro Duo 15 OLED lets you get things done in style: calmly, efficiently, and with zero fuss. It’s your powerful and elegant next-level companion for on-the-go productivity and creativity, featuring an amazing 4K OLED HDR touchscreen.",
                    ProductFeature="Color=black Height=9.81 inches Width=14.17 inches 2.34 kg"
                }
            };
        }

        [Fact]
        public async void GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockProductService.Setup(x => x.GetAllAsync()).ReturnsAsync(CustomResponseDto<List<ProductDto>>.Success(200, _products));

            var result = await _productsController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<List<ProductDto>>>(createActionResult.Value);

            Assert.Equal<int>(2, returnProducts.Data.Count);
        }

        [Theory]
        [InlineData("507f191e810c19729de860ea")]
        [InlineData("511f191e810c19729de860fr")]
        public async void GetById_IdValid_ReturnSuccessResult(string id)
        {
            var product = _products.First(x => x.Id == id);

            _mockProductService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CustomResponseDto<ProductDto>.Success(200, product));

            var result = await _productsController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductDto>>(createActionResult.Value);

            Assert.Equal(id, returnProducts.Data.Id);
            Assert.Equal(product.Name, returnProducts.Data.Name);
            Assert.Equal(product.Stock, returnProducts.Data.Stock);
            Assert.Equal(product.Price, returnProducts.Data.Price);
            Assert.Equal(product.Description, returnProducts.Data.Description);
            Assert.Equal(product.ProductFeature, returnProducts.Data.ProductFeature);
        }

        [Theory]
        [InlineData("501f191e810c19729de860ea")]
        public async void GetById_IdInValid_ReturnNotFoundException(string id)
        {
            _mockProductService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Product ({id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.GetById(id));

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product ({id}) not found!", exception.Message);
        }




    }
}
