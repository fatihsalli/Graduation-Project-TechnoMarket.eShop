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
            _mockMapper=new Mock<IMapper>();
            _mockLogger=new Mock<ILogger<ProductService>>();
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
        public async Task GetAll_Products_Success()
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

            var result=await _productService.GetAllAsync();

            var successResult=Assert.IsType<List<ProductWithCategoryDto>>(result);

            //Assert.Equal(_mo, result);

            var returnResponse=Assert.IsAssignableFrom<List<ProductWithCategoryDto>>(result);

            Assert.Equal(1, returnResponse.Count);
        }
























    }








}

