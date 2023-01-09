using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnoMarket.Services.Order.Controllers;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class OrdersControllerTest
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _ordersController;

        //Fake data
        private List<OrderDto> _orders;

        public OrdersControllerTest()
        {
            _mockOrderService = new Mock<IOrderService>();
            _ordersController = new OrdersController(_mockOrderService.Object);

            _orders = new List<OrderDto>()
            {
                new OrderDto()
                {
                    Id="2de578aa-d36d-41c3-8061-2dc64a8f765a",
                    CustomerId="3fa578aa-d36d-41c3-8061-2dc64a8f787c",
                    TotalPrice=2000,
                    Status="Active",
                    FullAddress = "Levent Turkey Istanbul 34",
                    OrderItems=new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                            Price=800,
                            ProductName="Iphone 14 Plus",
                            Quantity=1
                        },
                        new OrderItemDto
                        {
                            ProductId="46A02782-F572-4C86-860E-8F908FC105CE",
                            Price=1200,
                            ProductName="Asus Zenbook",
                            Quantity=1
                        }
                    }
                },
                new OrderDto()
                {
                    Id="3ad578aa-d36d-41c3-8061-2dc64a8f754f",
                    CustomerId="3fa578aa-d36d-41c3-8061-2dc64a8f787c",
                    TotalPrice=1600,
                    Status="Active",
                    FullAddress = "Merkez Turkey Isparta 34",
                    OrderItems=new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                            Price=800,
                            ProductName="Iphone 14 Plus",
                            Quantity=2
                        }
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

            //CustomerService'i taklit ettik.
            _mockCustomerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customerDto);

            var result = await _customersController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCustomer = Assert.IsAssignableFrom<CustomResponseDto<CustomerDto>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockCustomerService.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.Equal(id, returnCustomer.Data.Id);
            Assert.Equal(customerDto.FirstName, returnCustomer.Data.FirstName);
            Assert.Equal(customerDto.LastName, returnCustomer.Data.LastName);
            Assert.Equal(customerDto.Email, returnCustomer.Data.Email);
            Assert.Equal(customerDto.Address, returnCustomer.Data.Address);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCustomerService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Customer with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customersController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCustomerService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Customer with id ({id}) didn't find in the database.", exception.Message);
        }

        [Theory]
        [InlineData("sallifatih@hotmail.com")]
        [InlineData("kazim_onaran@hotmail.com")]
        public async Task GetByEmail_ActionExecute_ReturnSuccessResult(string email)
        {
            var customerDto = _customers.First(x => x.Email == email);

            //CustomerService'i taklit ettik.
            _mockCustomerService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(customerDto);

            var result = await _customersController.GetCustomerByEmail(email);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCustomer = Assert.IsAssignableFrom<CustomResponseDto<CustomerDto>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockCustomerService.Verify(x => x.GetByEmailAsync(email), Times.Once);
            Assert.Equal(customerDto.Id, returnCustomer.Data.Id);
            Assert.Equal(customerDto.FirstName, returnCustomer.Data.FirstName);
            Assert.Equal(customerDto.LastName, returnCustomer.Data.LastName);
            Assert.Equal(email, returnCustomer.Data.Email);
            Assert.Equal(customerDto.Address, returnCustomer.Data.Address);
        }

        [Theory]
        [InlineData("kerem_onaran_92@gmail.com")] //Invalid Email
        public async Task GetByEmail_IdNotFound_ReturnNotFoundException(string email)
        {
            _mockCustomerService.Setup(x => x.GetByEmailAsync(email)).Throws(new NotFoundException($"Customer with email ({email}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customersController.GetCustomerByEmail(email));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCustomerService.Verify(x => x.GetByEmailAsync(email), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Customer with email ({email}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_ActionExecute_ReturnSuccessResult()
        {
            var customerCreateDto = new CustomerCreateDto()
            {
                FirstName = "Hasan",
                LastName = "Yerebakan",
                Email = "hasanyerebakan@gmail.com",
                Address = new AddressDto
                {
                    AddressLine = "Merkez",
                    City = "Isparta",
                    Country = "Türkiye",
                    CityCode = 32
                }
            };

            var customerDto = new CustomerDto()
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                FirstName = "Hasan",
                LastName = "Yerebakan",
                Email = "hasanyerebakan@gmail.com",
                Address = new AddressDto
                {
                    AddressLine = "Merkez",
                    City = "Isparta",
                    Country = "Türkiye",
                    CityCode = 32
                }
            };

            _mockCustomerService.Setup(x => x.AddAsync(customerCreateDto)).ReturnsAsync(customerDto);

            var result = await _customersController.Create(customerCreateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnCustomer = Assert.IsAssignableFrom<CustomResponseDto<CustomerDto>>(createActionResult.Value);

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCustomerService.Verify(x => x.AddAsync(customerCreateDto), Times.Once);

            Assert.Equal(customerCreateDto.FirstName, returnCustomer.Data.FirstName);
            Assert.Equal(customerCreateDto.LastName, returnCustomer.Data.LastName);
            Assert.Equal(customerCreateDto.Email, returnCustomer.Data.Email);
            Assert.Equal(customerCreateDto.Address.AddressLine, returnCustomer.Data.Address.AddressLine);
            Assert.Equal(customerCreateDto.Address.City, returnCustomer.Data.Address.City);
            Assert.Equal(customerCreateDto.Address.Country, returnCustomer.Data.Address.Country);
            Assert.Equal(customerCreateDto.Address.CityCode, returnCustomer.Data.Address.CityCode);
        }

        [Fact]
        public async Task Update_ActionExecute_ReturnSuccessResult()
        {
            var customerUpdateDto = new CustomerUpdateDto()
            {
                Id = "81cae6a5-3ca4-42fd-9027-bd3cce250f6b",
                FirstName = "Kerem", //Değişen
                LastName = "Öztürk", //Değişen
                Email = "sallifatih@hotmail.com",
                Address = new AddressDto
                {
                    AddressLine = "Kadıköy",
                    City = "İstanbul",
                    Country = "Türkiye",
                    CityCode = 34
                }
            };

            //CustomerService'i taklit ettik.
            _mockCustomerService.Setup(x => x.UpdateAsync(customerUpdateDto));

            var result = await _customersController.Update(customerUpdateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockCustomerService.Verify(x => x.UpdateAsync(customerUpdateDto), Times.Once);
        }

        [Fact]
        public async Task Update_IdIsNotEqualProduct_ReturnNotFoundException()
        {
            var customerUpdateDto = new CustomerUpdateDto()
            {
                Id = "65cae6a5-3ca4-42fd-9027-bd3cce250f6a",
                FirstName = "Kerem", //Değişen
                LastName = "Öztürk", //Değişen
                Email = "sallifatih@hotmail.com",
                Address = new AddressDto
                {
                    AddressLine = "Kadıköy",
                    City = "İstanbul",
                    Country = "Türkiye",
                    CityCode = 34
                }
            };

            _mockCustomerService.Setup(x => x.UpdateAsync(customerUpdateDto)).Throws(new NotFoundException($"Customer ({customerUpdateDto.Id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customersController.Update(customerUpdateDto));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCustomerService.Verify(x => x.UpdateAsync(customerUpdateDto), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Customer ({customerUpdateDto.Id}) not found!", exception.Message);
        }


        [Theory]
        [InlineData("81cae6a5-3ca4-42fd-9027-bd3cce250f6b")]
        public async Task Delete_ActionExecute_ReturnSuccessResult(string id)
        {
            var customerDto = _customers.First(x => x.Id == id);

            _mockCustomerService.Setup(x => x.RemoveAsync(id));

            var result = await _customersController.Delete(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockCustomerService.Verify(x => x.RemoveAsync(id), Times.Once);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task Delete_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCustomerService.Setup(x => x.RemoveAsync(id)).Throws(new NotFoundException($"Customer with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customersController.Delete(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCustomerService.Verify(x => x.RemoveAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Customer with id ({id}) didn't find in the database.", exception.Message);
        }
    }
}
