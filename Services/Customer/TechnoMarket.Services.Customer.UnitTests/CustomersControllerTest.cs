using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnoMarket.Services.Customer.Controllers;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CustomersControllerTest
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomersController _customersController;

        //Fake data
        private List<CustomerDto> _customers;

        public CustomersControllerTest()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customersController = new CustomersController(_mockCustomerService.Object);

            _customers = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Id="81cae6a5-3ca4-42fd-9027-bd3cce250f6b",
                    FirstName= "Fatih",
                    LastName="Şallı",
                    Email= "sallifatih@hotmail.com",
                    Address=new AddressDto
                    {
                        AddressLine = "Kadıköy",
                        City = "İstanbul",
                        Country = "Türkiye",
                        CityCode = 34
                    }
                },
                new CustomerDto()
                {
                    Id="6e0dce4f-0d8c-4499-9283-6e008605b551",
                    FirstName= "Kazım",
                    LastName="Onaran",
                    Email= "kazim_onaran@hotmail.com",
                    Address=new AddressDto
                    {
                        AddressLine = "Levent",
                        City = "İstanbul",
                        Country = "Türkiye",
                        CityCode = 34
                    }
                }
            };
        }

        [Fact]
        public async Task GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockCustomerService.Setup(x => x.GetAllAsync()).ReturnsAsync(_customers);

            var result = await _customersController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<List<CustomerDto>>>(createActionResult.Value);

            Assert.Equal<int>(2, returnProducts.Data.Count);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockCustomerService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData("81cae6a5-3ca4-42fd-9027-bd3cce250f6b")]
        [InlineData("6e0dce4f-0d8c-4499-9283-6e008605b551")]
        public async Task GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var customerDto = _customers.First(x => x.Id == id);

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
            Assert.Equal(productDto.Picture, returnProducts.Data.Picture);
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
                Picture = "apple_iphone_14.jpeg",
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
                Picture = productCreateDto.Picture,
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
            Assert.Equal(productCreateDto.Picture, returnProducts.Data.Picture);
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
                Picture = "asuszenbook.jpeg",
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
                Picture = "asuszenbook.jpeg",
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
