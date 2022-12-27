using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class ProductServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPublishEndpoint> _mockPublisher;
        private readonly IProductService _productService;
        private List<Product> _products;

        public ProductServiceTest()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _mockPublisher= new Mock<IPublishEndpoint>();
            _productService = new ProductService(_mockMapper.Object, _mockRepo.Object, _mockUnitOfWork.Object, _mockLogger.Object,_mockPublisher.Object);
            _products = new List<Product>()
            {
                new Product
                {
                    Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
                    Name = "Asus Zenbook",
                    Stock = 10,
                    Price = 40000,
                    Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
                    ImageFile = "asuszenbook.jpeg",
                    CreatedAt = DateTime.Now,
                    Feature=new ProductFeature
                    {
                        Id = new Guid("46a02782-f572-4c86-860e-8f908fc105ce"),
                        Color = "Black",
                        Height = "12'",
                        Width = "15.3'",
                        Weight = "2.5 kg"
                    },
                    CategoryId=new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e74"),
                    Category=new Category
                    {
                        Id = new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e74"),
                        Name = "Notebook"
                    }
                }
            };
        }

        [Fact]
        public async Task Get_GetAllProducts_Success()
        {
            //Product Repository'i taklit ederek _products listesinin dönmesini sağladık.
            _mockRepo.Setup(x => x.GetProductsWithCategoryAndFeaturesAsync()).ReturnsAsync(_products);

            var productWithCategoryDtos = new List<ProductWithCategoryDto>()
            {
                new ProductWithCategoryDto
                {
                    Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                    Name="Asus Zenbook",
                    Stock = 10,
                    Price = 40000,
                    Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
                    ImageFile = "asuszenbook.jpeg",
                    ProductFeature="Black 15.3' 12' 2.5 kg",
                    Category=new CategoryDto{Id="43f0db4e-08df-40d0-bb74-c8349f9f2e74",Name="Notebook"}
                }
            };

            //Mapper'ı taklit ederek List<ProductWithCategoryDto> oluşturduk
            _mockMapper.Setup(x => x.Map<List<ProductWithCategoryDto>>(_products)).Returns(productWithCategoryDtos);

            var result = await _productService.GetAllAsync();

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockRepo.Verify(x => x.GetProductsWithCategoryAndFeaturesAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<List<ProductWithCategoryDto>>(_products), Times.Once);

            //Result tipini kontrol ediyoruz.
            Assert.IsAssignableFrom<List<ProductWithCategoryDto>>(result);

            //Mapper ile gönderdiğimiz data ile resulttan gelen datayı check ediyoruz.
            Assert.Equal(productWithCategoryDtos, result);

            //Bir tane ürün eklediğimiz için bir tane olup olmadığını kontrol ediyoruz. Birden fazla olması durumunda "Assert.Equal<int>(2, result.Count);" olarak kullanabiliriz.
            Assert.Single(result);
        }

        [Theory]
        [InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        public async Task GetById_GetProduct_Success(string id)
        {
            //Kendi oluşturduğumuz listten ilgili id'yi bulduk.
            var product = _products.First(x => x.Id == new Guid(id));

            //Product Repository'i taklit ederek geriye bu product'ı döndük.
            _mockRepo.Setup(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id)).ReturnsAsync(product);

            var productWithCategoryDtos = new ProductWithCategoryDto
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = "Asus Zenbook",
                Stock = 10,
                Price = 40000,
                Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
                ImageFile = "asuszenbook.jpeg",
                ProductFeature = "Black 15.3' 12' 2.5 kg",
                Category = new CategoryDto { Id = "43f0db4e-08df-40d0-bb74-c8349f9f2e74", Name = "Notebook" }
            };

            //Mapper'ı taklit ederek "ProductWithCategoryDto" oluşturduk.
            _mockMapper.Setup(x => x.Map<ProductWithCategoryDto>(product)).Returns(productWithCategoryDtos);

            var result = await _productService.GetByIdAsync(id);

            _mockRepo.Verify(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id), Times.Once);
            _mockMapper.Verify(x => x.Map<ProductWithCategoryDto>(product), Times.Once);
            Assert.Equal(product.Id.ToString(), result.Id);
            Assert.IsAssignableFrom<ProductWithCategoryDto>(result);
            Assert.Equal(productWithCategoryDtos, result);
        }

        [Theory]
        [InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        public async Task GetById_IdNotFound_ReturnException(string id)
        {
            Product product = null;

            _mockRepo.Setup(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id)).ReturnsAsync(product);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.GetByIdAsync(id));

            _mockRepo.Verify(x => x.GetSingleProductByIdWithCategoryAndFeaturesAsync(id), Times.Once);
            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_CreateProduct_Success()
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

            var product = new Product()
            {
                //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
                Name = productCreateDto.Name,
                Stock = productCreateDto.Stock,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description,
                ImageFile = productCreateDto.ImageFile,
                CreatedAt = DateTime.Now,
                Feature = new ProductFeature
                {
                    //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
                    Color = "Black",
                    Height = "5'",
                    Width = "4.5'",
                    Weight = "0.5 kg"
                },
                CategoryId = new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e72"),
                Category = new Category
                {
                    Id = new Guid("43f0db4e-08df-40d0-bb74-c8349f9f2e72"),
                    Name = "SmartPhone"
                }
            };

            _mockMapper.Setup(x => x.Map<Product>(productCreateDto)).Returns(product);

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

            _mockRepo.Setup(x => x.AddAsync(product));
            _mockUnitOfWork.Setup(x => x.CommitAsync());
            _mockMapper.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            var result = await _productService.AddAsync(productCreateDto);

            _mockRepo.Verify(x => x.AddAsync(product), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<ProductDto>(product), Times.Once);
            _mockMapper.Verify(x => x.Map<Product>(productCreateDto), Times.Once);
            Assert.IsAssignableFrom<ProductDto>(result);
            Assert.Equal(productDto, result);
            Assert.Equal(productCreateDto.Name, result.Name);
            Assert.Equal(productCreateDto.Stock, result.Stock);
            Assert.Equal(productCreateDto.Price, result.Price);
            Assert.Equal(productCreateDto.Description, result.Description);
            Assert.Equal(productCreateDto.ImageFile, result.ImageFile);
            Assert.Equal(productCreateDto.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task Update_UpdateProduct_Success()
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

            var product = _products.Where(x => x.Id == new Guid(productUpdateDto.Id)).SingleOrDefault();

            product.Name = productUpdateDto.Name;
            product.Stock = productUpdateDto.Stock;
            product.Price = productUpdateDto.Price;
            product.Description = productUpdateDto.Description;
            product.ImageFile = productUpdateDto.ImageFile;
            product.CategoryId = new Guid(productUpdateDto.CategoryId);
            product.Feature = new ProductFeature
            {
                Id = new Guid(productUpdateDto.Id),
                Color = productUpdateDto.Feature.Color,
                Height = productUpdateDto.Feature.Height,
                Width = productUpdateDto.Feature.Width,
                Weight = productUpdateDto.Feature.Weight,
            };

            _mockMapper.Setup(x => x.Map<Product>(productUpdateDto)).Returns(product);

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id))).ReturnsAsync(true);
            _mockRepo.Setup(x => x.Update(product)).Returns(product);
            _mockUnitOfWork.Setup(x => x.CommitAsync());

            await _productService.UpdateAsync(productUpdateDto);

            _mockRepo.Verify(x => x.Update(product), Times.Once);
            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id)), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<Product>(productUpdateDto));
            Assert.Equal(_products.First().Name, productUpdateDto.Name);
            Assert.Equal(_products.First().Price, productUpdateDto.Price);
            Assert.Equal(_products.First().Description, productUpdateDto.Description);
        }

        [Fact]
        public async Task Update_IdNotFound_ReturnException()
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

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id))).ReturnsAsync(false);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.UpdateAsync(productUpdateDto));

            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id)), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Product with id ({productUpdateDto.Id}) didn't find in the database.", exception.Message);
        }

        [Theory]
        [InlineData("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d")]
        public async Task Remove_RemoveProduct_Success(string id)
        {
            var product = _products.First(x => x.Id == new Guid(id));

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);
            _mockRepo.Setup(x => x.Remove(product));

            await _productService.RemoveAsync(id);

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            _mockRepo.Verify(x => x.Remove(product), Times.Once);
        }

        [Theory]
        [InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        public async Task Remove_IdNotFound_ReturnException(string id)
        {
            Product product = null;

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _productService.RemoveAsync(id));

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Product with id ({id}) didn't find in the database.", exception.Message);
        }
    }
}

