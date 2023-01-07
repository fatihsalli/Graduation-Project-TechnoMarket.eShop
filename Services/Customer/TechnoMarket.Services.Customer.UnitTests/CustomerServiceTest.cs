using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;
using Xunit;
using CustomerEntity = TechnoMarket.Services.Customer.Models.Customer;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CustomerServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CustomerService>> _mockLogger;
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ICustomerService _customerService;
        private List<CustomerEntity> _customers;

        public CustomerServiceTest()
        {
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CustomerService>>();
            _mockRepo = new Mock<ICustomerRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _customerService = new CustomerService(_mockMapper.Object, _mockRepo.Object, _mockUnitOfWork.Object, _mockLogger.Object);

            _customers = new List<CustomerEntity>()
            {
                new CustomerEntity
                {
                    Id=new Guid("81cae6a5-3ca4-42fd-9027-bd3cce250f6b"),
                    FirstName= "Fatih",
                    LastName="Şallı",
                    Email= "sallifatih@hotmail.com",
                    CreatedAt= DateTime.Now,
                    Address=new Address
                    {
                        AddressLine = "Kadıköy",
                        City = "İstanbul",
                        Country = "Türkiye",
                        CityCode = 34
                    }
                },
                new CustomerEntity
                {
                    Id=new Guid("6e0dce4f-0d8c-4499-9283-6e008605b551"),
                    FirstName= "Kazım",
                    LastName="Onaran",
                    Email= "kazim_onaran@hotmail.com",
                    CreatedAt= DateTime.Now,
                    Address=new Address
                    {
                        AddressLine = "Levent",
                        City = "İstanbul",
                        Country = "Türkiye",
                        CityCode = 34
                    }
                }
            };
        }

        [Theory]
        [InlineData("6e0dce4f-0d8c-4499-9283-6e008605b551")]
        public async Task GetById_GetCustomer_Success(string id)
        {
            //Kendi oluşturduğumuz listten ilgili id'yi bulduk.
            var customer = _customers.First(x => x.Id == new Guid(id));

            //Customer Repository'i taklit ederek geriye bu product'ı döndük.
            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customer);

            var customerDto = new CustomerDto
            {
                Id = "6e0dce4f-0d8c-4499-9283-6e008605b551",
                FirstName = "Kazım",
                LastName = "Onaran",
                Email = "kazim_onaran@hotmail.com",
                Address = new AddressDto
                {
                    AddressLine = "Levent",
                    City = "İstanbul",
                    Country = "Türkiye",
                    CityCode = 34
                }
            };

            //Mapper'ı taklit ederek "ProductWithCategoryDto" oluşturduk.
            _mockMapper.Setup(x => x.Map<CustomerDto>(customer)).Returns(customerDto);

            var result = await _customerService.GetByIdAsync(id);

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            _mockMapper.Verify(x => x.Map<CustomerDto>(customer), Times.Once);
            Assert.Equal(customer.Id.ToString(), result.Id);
            Assert.IsAssignableFrom<CustomerDto>(result);
            Assert.Equal(customerDto, result);
        }

        [Theory]
        [InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        public async Task GetById_IdNotFound_ReturnException(string id)
        {
            CustomerEntity customer = null;

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customer);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customerService.GetByIdAsync(id));

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Customer with id ({id}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_CreateCustomer_Success()
        {
            var customerCreateDto = new CustomerCreateDto()
            {
                FirstName="Hasan",
                LastName="Yerebakan",
                Email="hasanyerebakan@gmail.com",
                Address=new AddressDto
                {
                    AddressLine = "Merkez",
                    City = "Isparta",
                    Country = "Türkiye",
                    CityCode = 32
                }
            };

            var customer = new CustomerEntity()
            {
                //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
                FirstName = "Hasan",
                LastName = "Yerebakan",
                Email = "hasanyerebakan@gmail.com",
                CreatedAt = DateTime.Now,
                Address = new Address
                {
                    AddressLine = "Merkez",
                    City = "Isparta",
                    Country = "Türkiye",
                    CityCode = 32
                }
            };

            _mockMapper.Setup(x => x.Map<CustomerEntity>(customerCreateDto)).Returns(customer);

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

            _mockRepo.Setup(x => x.AddAsync(customer));
            _mockUnitOfWork.Setup(x => x.CommitAsync());
            _mockMapper.Setup(x => x.Map<CustomerDto>(customer)).Returns(customerDto);

            var result = await _customerService.AddAsync(customerCreateDto);

            _mockRepo.Verify(x => x.AddAsync(customer), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<CustomerDto>(customer), Times.Once);
            _mockMapper.Verify(x => x.Map<CustomerEntity>(customerCreateDto), Times.Once);
            Assert.IsAssignableFrom<CustomerDto>(result);
            Assert.Equal(customerDto, result);
            Assert.Equal(customerDto.FirstName, result.FirstName);
            Assert.Equal(customerDto.LastName, result.LastName);
            Assert.Equal(customerDto.Email, result.Email);
            Assert.Equal(customerDto.Address, result.Address);
        }

        [Fact]
        public async Task Update_UpdateCustomer_Success()
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

            var customer = _customers.Where(x => x.Id == new Guid(customerUpdateDto.Id)).SingleOrDefault();

            customer.FirstName = customerUpdateDto.FirstName;
            customer.LastName = customerUpdateDto.LastName;
            customer.Email = customerUpdateDto.Email;
            customer.Address.AddressLine = customerUpdateDto.Address.AddressLine;
            customer.Address.City = customerUpdateDto.Address.City;
            customer.Address.Country = customerUpdateDto.Address.Country;
            customer.Address.CityCode = customerUpdateDto.Address.CityCode;

            _mockMapper.Setup(x => x.Map<CustomerEntity>(customerUpdateDto)).Returns(customer);

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(customerUpdateDto.Id))).ReturnsAsync(true);
            _mockRepo.Setup(x => x.Update(customer));
            _mockUnitOfWork.Setup(x => x.CommitAsync());

            await _customerService.UpdateAsync(customerUpdateDto);

            _mockRepo.Verify(x => x.Update(customer), Times.Once);
            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(customerUpdateDto.Id)), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<CustomerEntity>(customerUpdateDto));

            Assert.Equal(_customers.First().FirstName, customerUpdateDto.FirstName);
            Assert.Equal(_customers.First().LastName, customerUpdateDto.LastName);
        }

        [Fact]
        public async Task Update_IdNotFound_ReturnException()
        {
            var customerUpdateDto = new CustomerUpdateDto()
            {
                Id = "65cae6a5-3ca4-42fd-9027-bd3cce250f6b", //Id Invalid
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

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(customerUpdateDto.Id))).ReturnsAsync(false);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _customerService.UpdateAsync(customerUpdateDto));

            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(customerUpdateDto.Id)), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Customer with id ({customerUpdateDto.Id}) didn't find in the database.", exception.Message);
        }

        //[Theory]
        //[InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        //public async Task Remove_RemoveProduct_Success(string id)
        //{
        //    var product = _products.First(x => x.Id == new Guid(id));

        //    _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);
        //    _mockRepo.Setup(x => x.Remove(product));

        //    await _productService.RemoveAsync(id);

        //    _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
        //    _mockRepo.Verify(x => x.Remove(product), Times.Once);
        //}

        //[Theory]
        //[InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        //public async Task Remove_IdNotFound_ReturnException(string id)
        //{
        //    Product product = null;

        //    _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);

        //    Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.RemoveAsync(id));

        //    _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);

        //    Assert.IsType<NotFoundException>(exception);
        //    Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        //}
    }
}

