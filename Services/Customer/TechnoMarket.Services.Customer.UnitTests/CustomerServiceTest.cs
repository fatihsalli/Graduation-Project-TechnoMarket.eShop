using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
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

        [Fact]
        public async Task Get_GetAllCustomers_Success()
        {
            //Customer Repository'i taklit ederek _customer listesinin dönmesini sağladık.
            _mockRepo.Setup(x => x.GetAll()).Returns(_customers.AsQueryable());

            var customerDtos = new List<CustomerDto>()
            {
                new CustomerDto
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
                new CustomerDto
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

            //Mapper'ı taklit ederek List<CustomerDto> oluşturduk
            _mockMapper.Setup(x => x.Map<List<CustomerDto>>(_customers)).Returns(customerDtos);

            var result = await _customerService.GetAllAsync();

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockRepo.Verify(x => x.GetAll(), Times.Once);
            _mockMapper.Verify(x => x.Map<List<CustomerDto>>(_customers), Times.Once);

            //Result tipini kontrol ediyoruz.
            Assert.IsAssignableFrom<List<CustomerDto>>(result);

            //Mapper ile gönderdiğimiz data ile resulttan gelen datayı check ediyoruz.
            Assert.Equal(customerDtos, result);

            //Bir tane ürün eklediğimiz için bir tane olup olmadığını kontrol ediyoruz. Birden fazla olması durumunda "Assert.Equal<int>(2, result.Count);" olarak kullanabiliriz.
            Assert.Equal<int>(2,result.Count);
        }

        //[Theory]
        //[InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        //public async Task GetById_GetProduct_Success(string id)
        //{
        //    //Kendi oluşturduğumuz listten ilgili id'yi bulduk.
        //    var product = _products.First(x => x.Id == new Guid(id));

        //    //Product Repository'i taklit ederek geriye bu product'ı döndük.
        //    _mockRepo.Setup(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id)).ReturnsAsync(product);

        //    var productWithCategoryDtos = new ProductWithCategoryDto
        //    {
        //        Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
        //        Name = "Asus Zenbook",
        //        Stock = 10,
        //        Price = 40000,
        //        Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
        //        Picture = "asuszenbook.jpeg",
        //        ProductFeature = "Black 15.3' 12' 2.5 kg",
        //        Category = new CategoryDto { Id = "43f0db4e-08df-40d0-bb74-c8349f9f2e74", Name = "Notebook" }
        //    };

        //    //Mapper'ı taklit ederek "ProductWithCategoryDto" oluşturduk.
        //    _mockMapper.Setup(x => x.Map<ProductWithCategoryDto>(product)).Returns(productWithCategoryDtos);

        //    var result = await _productService.GetByIdAsync(id);

        //    _mockRepo.Verify(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id), Times.Once);
        //    _mockMapper.Verify(x => x.Map<ProductWithCategoryDto>(product), Times.Once);
        //    Assert.Equal(product.Id.ToString(), result.Id);
        //    Assert.IsAssignableFrom<ProductWithCategoryDto>(result);
        //    Assert.Equal(productWithCategoryDtos, result);
        //}

        //[Theory]
        //[InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        //public async Task GetById_IdNotFound_ReturnException(string id)
        //{
        //    Product product = null;

        //    _mockRepo.Setup(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id)).ReturnsAsync(product);

        //    Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.GetByIdAsync(id));

        //    _mockRepo.Verify(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id), Times.Once);
        //    Assert.IsType<NotFoundException>(exception);
        //    Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        //}

        //[Fact]
        //public async Task Create_CreateProduct_Success()
        //{
        //    var productCreateDto = new ProductCreateDto()
        //    {
        //        Name = "Iphone 14",
        //        Stock = 10,
        //        Price = 750.00M,
        //        Description = "Last model smart phone",
        //        Picture = "apple_iphone_14.jpeg",
        //        CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e72",
        //        Feature = new ProductFeatureDto { Color = "Black", Height = "5'", Weight = "4.5'", Width = "0.5 kg" }
        //    };

        //    var product = new Product()
        //    {
        //        //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
        //        Name = productCreateDto.Name,
        //        Stock = productCreateDto.Stock,
        //        Price = productCreateDto.Price,
        //        Description = productCreateDto.Description,
        //        Picture = productCreateDto.Picture,
        //        CreatedAt = DateTime.Now,
        //        Feature = new ProductFeature
        //        {
        //            //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
        //            Color = "Black",
        //            Height = "5'",
        //            Width = "4.5'",
        //            Weight = "0.5 kg"
        //        },
        //        CategoryId = new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e72"),
        //        Category = new Category
        //        {
        //            Id = new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e72"),
        //            Name = "SmartPhone"
        //        }
        //    };

        //    _mockMapper.Setup(x => x.Map<Product>(productCreateDto)).Returns(product);

        //    var productDto = new ProductDto()
        //    {
        //        Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
        //        Name = productCreateDto.Name,
        //        Stock = productCreateDto.Stock,
        //        Price = productCreateDto.Price,
        //        Description = productCreateDto.Description,
        //        Picture = productCreateDto.Picture,
        //        ProductFeature = "Black 5' 4.5' 0.5 kg",
        //        CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e72",
        //    };

        //    _mockRepo.Setup(x => x.AddAsync(product));
        //    _mockUnitOfWork.Setup(x => x.CommitAsync());
        //    _mockMapper.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

        //    var result = await _productService.AddAsync(productCreateDto);

        //    _mockRepo.Verify(x => x.AddAsync(product), Times.Once);
        //    _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        //    _mockMapper.Verify(x => x.Map<ProductDto>(product), Times.Once);
        //    _mockMapper.Verify(x => x.Map<Product>(productCreateDto), Times.Once);
        //    Assert.IsAssignableFrom<ProductDto>(result);
        //    Assert.Equal(productDto, result);
        //    Assert.Equal(productCreateDto.Name, result.Name);
        //    Assert.Equal(productCreateDto.Stock, result.Stock);
        //    Assert.Equal(productCreateDto.Price, result.Price);
        //    Assert.Equal(productCreateDto.Description, result.Description);
        //    Assert.Equal(productCreateDto.Picture, result.Picture);
        //    Assert.Equal(productCreateDto.CategoryId, result.CategoryId);
        //}

        //[Fact]
        //public async Task Update_UpdateProduct_Success()
        //{
        //    var productUpdateDto = new ProductUpdateDto()
        //    {
        //        Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
        //        Name = "Asus Zenbook V002",
        //        Stock = 10,
        //        Price = 35000,
        //        Description = "Unit test description",
        //        Picture = "asuszenbook.jpeg",
        //        CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e74",
        //        Feature = new ProductFeatureDto { Color = "Black", Height = "12'", Weight = "15.3'", Width = "2.5 kg" }
        //    };

        //    var product = _products.Where(x => x.Id == new Guid(productUpdateDto.Id)).SingleOrDefault();

        //    product.Name = productUpdateDto.Name;
        //    product.Stock = productUpdateDto.Stock;
        //    product.Price = productUpdateDto.Price;
        //    product.Description = productUpdateDto.Description;
        //    product.Picture = productUpdateDto.Picture;
        //    product.CategoryId = new Guid(productUpdateDto.CategoryId);
        //    product.Feature = new ProductFeature
        //    {
        //        Id = new Guid(productUpdateDto.Id),
        //        Color = productUpdateDto.Feature.Color,
        //        Height = productUpdateDto.Feature.Height,
        //        Width = productUpdateDto.Feature.Width,
        //        Weight = productUpdateDto.Feature.Weight,
        //    };

        //    _mockMapper.Setup(x => x.Map<Product>(productUpdateDto)).Returns(product);

        //    _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id))).ReturnsAsync(true);
        //    _mockRepo.Setup(x => x.Update(product)).Returns(product);
        //    _mockUnitOfWork.Setup(x => x.CommitAsync());

        //    await _productService.UpdateAsync(productUpdateDto);

        //    _mockRepo.Verify(x => x.Update(product), Times.Once);
        //    _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id)), Times.Once);
        //    _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        //    _mockMapper.Verify(x => x.Map<Product>(productUpdateDto));
        //    Assert.Equal(_products.First().Name, productUpdateDto.Name);
        //    Assert.Equal(_products.First().Price, productUpdateDto.Price);
        //    Assert.Equal(_products.First().Description, productUpdateDto.Description);
        //}

        //[Fact]
        //public async Task Update_IdNotFound_ReturnException()
        //{
        //    var productUpdateDto = new ProductUpdateDto()
        //    {
        //        Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
        //        Name = "Asus Zenbook V002",
        //        Stock = 10,
        //        Price = 35000,
        //        Description = "Unit test description",
        //        Picture = "asuszenbook.jpeg",
        //        CategoryId = "43f0db4e-08df-40d0-bb74-c8349f9f2e74",
        //        Feature = new ProductFeatureDto { Color = "Black", Height = "12'", Weight = "15.3'", Width = "2.5 kg" }
        //    };

        //    _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id))).ReturnsAsync(false);

        //    Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.UpdateAsync(productUpdateDto));

        //    _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id)), Times.Once);

        //    Assert.IsType<NotFoundException>(exception);
        //    Assert.Equal($"Product with id ({productUpdateDto.Id}) didn't find in the database.", exception.Message);
        //}

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

