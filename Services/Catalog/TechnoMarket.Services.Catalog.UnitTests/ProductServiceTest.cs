using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class ProductServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IProductService _productService;
        private List<Product> _products;

        public ProductServiceTest()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_mockMapper.Object, _mockRepo.Object, _mockUnitOfWork.Object, _mockLogger.Object);
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

            _mockMapper.Setup(x => x.Map<List<ProductWithCategoryDto>>(_products)).Returns(productWithCategoryDtos);

            var result = await _productService.GetAllAsync();

            Assert.IsAssignableFrom<List<ProductWithCategoryDto>>(result);
            Assert.Equal(productWithCategoryDtos, result);
            Assert.Single(result);
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
            _mockRepo.Setup(x => x.AddAsync(product));
            _mockUnitOfWork.Setup(x => x.CommitAsync());

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

            _mockMapper.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            var result = await _productService.AddAsync(productCreateDto);

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
            var product=_products.First();

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

            product.Name= productUpdateDto.Name;
            product.Price= productUpdateDto.Price;
            product.Description= productUpdateDto.Description;

            _mockMapper.Setup(x => x.Map<Product>(productUpdateDto)).Returns(product);

            _mockRepo.Setup(x => x.Update(product));
            _mockRepo.Verify(x=>x.Update(product),Times.Once);

            _mockUnitOfWork.Setup(x => x.CommitAsync());
            _mockRepo.Verify(x => x.Update(product), Times.Once);

            await _productService.UpdateAsync(productUpdateDto);

            Assert.Equal(_products.First().Name, productUpdateDto.Name);
            Assert.Equal(_products.First().Price, productUpdateDto.Price);
            Assert.Equal(_products.First().Description, productUpdateDto.Description);
        }





















    }








}

