using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnoMarket.Services.Catalog.Controllers;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class ProductsControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _productsController;

        //Fake data
        private List<ProductWithCategoryDto> _products;

        public ProductsControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _productsController = new ProductsController(_mockProductService.Object);

            _products = new List<ProductWithCategoryDto>()
            {
                new ProductWithCategoryDto()
                {
                    Id="3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                    Name="Iphone X",
                    Stock=10,
                    Price=950.00M,
                    Description="Super Retina HD display and A11 Bionic chip with 64-bit architecture",
                    ImageFile="apple_iphone.jpeg",
                    ProductFeature="Color=black Height=5.65 inches Width=2.79 inches 0.6 kg",
                    Category=new CategoryDto
                    {
                        Id = "15f0db4e-08df-40d0-bb74-c8349f9f2e62",
                        Name = "Smart Phone"
                    }                   
                },
                new ProductWithCategoryDto()
                {
                    Id="412ca3fc-e45b-4857-9950-2ff2a8e5915e",
                    Name="Asus Zenbook Pro Duo 15",
                    Stock=6,
                    Price=3500.00M,
                    Description="ZenBook Pro Duo 15 OLED lets you get things done in style: calmly, efficiently, and with zero fuss.",
                    ImageFile="asus_zenbook.jpeg",
                    ProductFeature="Color=black Height=9.81 inches Width=14.17 inches 2.34 kg",
                    Category=new CategoryDto
                    {
                        Id = "43f0db4e-08df-40d0-bb74-c8349f9f2e74",
                        Name = "Notebook"
                    }
                }
            };
        }

        [Fact]
        public async Task GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockProductService.Setup(x => x.GetAllAsync()).ReturnsAsync(_products);

            var result = await _productsController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<List<ProductWithCategoryDto>>>(createActionResult.Value);

            Assert.Equal<int>(2, returnProducts.Data.Count);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockProductService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        [InlineData("412ca3fc-e45b-4857-9950-2ff2a8e5915e")]
        public async Task GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var productDto = _products.First(x => x.Id == id);

            //ProductService'i taklit ettik.
            _mockProductService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(productDto);

            var result = await _productsController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductWithCategoryDto>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockProductService.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.Equal(id, returnProducts.Data.Id);
            Assert.Equal(productDto.Name, returnProducts.Data.Name);
            Assert.Equal(productDto.Stock, returnProducts.Data.Stock);
            Assert.Equal(productDto.Price, returnProducts.Data.Price);
            Assert.Equal(productDto.Description, returnProducts.Data.Description);
            Assert.Equal(productDto.ImageFile, returnProducts.Data.ImageFile);
            Assert.Equal(productDto.ProductFeature, returnProducts.Data.ProductFeature);
            Assert.Equal(productDto.Category, returnProducts.Data.Category);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockProductService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Product with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_ActionExecute_ReturnSuccessResult()
        {
            var productCreateDto = new ProductCreateDto()
            {
                Name = "Iphone 14",
                Stock = 10,
                Price = 750.00M,
                Description = "Last model smart phone",
                ImageFile = "apple_iphone_14.jpeg",
                CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e72",
                Feature = new ProductFeatureDto { Color = "Black", Height = "5'", Weight = "4.5'", Width = "0.5 kg" }
            };

            var productDto = new ProductDto()
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = productCreateDto.Name,
                Stock = productCreateDto.Stock,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description,
                ImageFile = productCreateDto.ImageFile,
                ProductFeature = "Black 5' 4.5' 0.5 kg",
                CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e72",
            };

            _mockProductService.Setup(x => x.AddAsync(productCreateDto)).ReturnsAsync(productDto);

            var result = await _productsController.Create(productCreateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<ProductDto>>(createActionResult.Value);

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.AddAsync(productCreateDto), Times.Once);

            Assert.Equal(productCreateDto.Name, returnProducts.Data.Name);
            Assert.Equal(productCreateDto.Stock, returnProducts.Data.Stock);
            Assert.Equal(productCreateDto.Price, returnProducts.Data.Price);
            Assert.Equal(productCreateDto.Description, returnProducts.Data.Description);
            Assert.Equal(productCreateDto.ImageFile, returnProducts.Data.ImageFile);
            Assert.Equal(productCreateDto.CategoryId, returnProducts.Data.CategoryId);
        }

        [Fact]
        public async Task Update_ActionExecute_ReturnSuccessResult()
        {
            var productUpdateDto = new ProductUpdateDto()
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = "Asus Zenbook V002",
                Stock = 10,
                Price = 35000,
                Description = "Unit test description",
                ImageFile = "asuszenbook.jpeg",
                CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e74",
                Feature = new ProductFeatureDto { Color = "Black", Height = "12'", Weight = "15.3'", Width = "2.5 kg" }
            };

            //ProductService'i taklit ettik.
            _mockProductService.Setup(x => x.UpdateAsync(productUpdateDto));

            var result = await _productsController.Update(productUpdateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockProductService.Verify(x => x.UpdateAsync(productUpdateDto), Times.Once);
        }

        [Fact]
        public async Task Update_IdIsNotEqualProduct_ReturnNotFoundException()
        {
            var productUpdateDto = new ProductUpdateDto()
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = "Asus Zenbook V002",
                Stock = 10,
                Price = 35000,
                Description = "Unit test description",
                ImageFile = "asuszenbook.jpeg",
                CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e74",
                Feature = new ProductFeatureDto { Color = "Black", Height = "12'", Weight = "15.3'", Width = "2.5 kg" }
            };

            _mockProductService.Setup(x => x.UpdateAsync(productUpdateDto)).Throws(new NotFoundException($"Product ({productUpdateDto.Id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.Update(productUpdateDto));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.UpdateAsync(productUpdateDto), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Product ({productUpdateDto.Id}) not found!", exception.Message);
        }


        [Theory]
        [InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        public async Task Delete_ActionExecute_ReturnSuccessResult(string id)
        {
            var productDto = _products.First(x => x.Id == id);

            _mockProductService.Setup(x => x.RemoveAsync(id));

            var result = await _productsController.Delete(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockProductService.Verify(x => x.RemoveAsync(id), Times.Once);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task Delete_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockProductService.Setup(x => x.RemoveAsync(id)).Throws(new NotFoundException($"Product with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productsController.Delete(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockProductService.Verify(x => x.RemoveAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        }
    }
}
