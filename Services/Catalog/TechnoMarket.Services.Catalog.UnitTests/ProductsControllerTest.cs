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
        public async void GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var productDto = _products.First(x => x.Id == id);

            //ProductService'i taklit ettik.
            _mockProductService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CustomResponseDto<ProductDto>.Success(200, productDto));

            var result = await _productsController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductDto>>(createActionResult.Value);

            Assert.Equal(id, returnProducts.Data.Id);
            Assert.Equal(productDto.Name, returnProducts.Data.Name);
            Assert.Equal(productDto.Stock, returnProducts.Data.Stock);
            Assert.Equal(productDto.Price, returnProducts.Data.Price);
            Assert.Equal(productDto.Description, returnProducts.Data.Description);
            Assert.Equal(productDto.ProductFeature, returnProducts.Data.ProductFeature);
        }

        [Theory]
        [InlineData("501f191e810c19729de860ab")] //Invalid Id
        public async void GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockProductService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Product ({id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product ({id}) not found!", exception.Message);
        }

        [Fact]
        public async void Create_ActionExecute_ReturnSuccessResult()
        {
            var productCreateDto=new ProductCreateDto() 
            { 
                Name="Iphone 14",
                Stock=10,
                Price=750.00M,
                Description="Last model smart phone"            
            };

            var productDto = new ProductDto()
            {
                Name = productCreateDto.Name,
                Stock = productCreateDto.Stock,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description
            };

            _mockProductService.Setup(x => x.CreateAsync(productCreateDto)).ReturnsAsync(CustomResponseDto<ProductDto>.Success(201, productDto));

            var result = await _productsController.Create(productCreateDto);

            _mockProductService.Verify(x => x.CreateAsync(productCreateDto), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductDto>>(createActionResult.Value);

            Assert.Equal(productCreateDto.Name, returnProducts.Data.Name);
            Assert.Equal(productCreateDto.Stock, returnProducts.Data.Stock);
            Assert.Equal(productCreateDto.Price, returnProducts.Data.Price);
            Assert.Equal(productCreateDto.Description, returnProducts.Data.Description);
        }

        [Fact]
        public async void Update_ActionExecute_ReturnSuccessResult()
        {
            var productDto = _products.First();
            var productUpdateDto = new ProductUpdateDto
            {
                Id = productDto.Id,
                //Senaryo=> İsmini güncelledik
                Name = "Iphone 14",
                Stock = productDto.Stock,
                Price = productDto.Price,
                Description = productDto.Description
            };
            var productToReturn = productDto;
            productToReturn.Name = productUpdateDto.Name;

            //ProductService'i taklit ettik.
            _mockProductService.Setup(x => x.UpdateAsync(productUpdateDto)).ReturnsAsync(CustomResponseDto<ProductDto>.Success(200, productToReturn));

            var result = await _productsController.Update(productUpdateDto);

            _mockProductService.Verify(x => x.UpdateAsync(productUpdateDto), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductDto>>(createActionResult.Value);

            Assert.Equal(productUpdateDto.Id, returnProducts.Data.Id);
            Assert.Equal(productUpdateDto.Name, returnProducts.Data.Name);
            Assert.Equal(productUpdateDto.Stock, returnProducts.Data.Stock);
            Assert.Equal(productUpdateDto.Price, returnProducts.Data.Price);
            Assert.Equal(productUpdateDto.Description, returnProducts.Data.Description);
        }

        [Fact]
        public async void Update_IdIsNotEqualProduct_ReturnNotFoundException()
        {
            var productDto = _products.First();
            var productUpdateDto = new ProductUpdateDto
            {
                //Invalid Id
                Id = "501f191e810c19729de860ea",
                Name = productDto.Name,
                Stock = productDto.Stock,
                Price = productDto.Price,
                Description = productDto.Description
            };

            _mockProductService.Setup(x => x.UpdateAsync(productUpdateDto)).Throws(new NotFoundException($"Product ({productUpdateDto.Id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.Update(productUpdateDto));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x=> x.UpdateAsync(productUpdateDto), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product ({productUpdateDto.Id}) not found!", exception.Message);
        }

        [Theory]
        [InlineData("507f191e810c19729de860ea")]
        [InlineData("511f191e810c19729de860fr")]
        public async void Delete_ActionExecute_ReturnSuccessResult(string id)
        {
            var productDto = _products.First(x => x.Id == id);

            _mockProductService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(CustomResponseDto<NoContentDto>.Success(204));

            var result = await _productsController.Delete(id);

            _mockProductService.Verify(x => x.DeleteAsync(id), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);
        }

        [Theory]
        [InlineData("501f191e810c19729de860ab")] //Wrong id
        public async void Delete_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockProductService.Setup(x => x.DeleteAsync(id)).Throws(new NotFoundException($"Product ({id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.Delete(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.DeleteAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product ({id}) not found!", exception.Message);
        }


    }
}
