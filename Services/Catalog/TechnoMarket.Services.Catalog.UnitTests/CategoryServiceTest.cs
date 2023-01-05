using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Services.Catalog.Dtos;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CategoryServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IGenericRepository<Category>> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<CategoryService>> _mockLogger;        
        private readonly ICategoryService _categoryService;
        private IQueryable<Category> _categories;

        public CategoryServiceTest()
        {
            _mockRepo = new Mock<IGenericRepository<Category>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryService>>();
            _categoryService = new CategoryService(_mockMapper.Object,_mockRepo.Object,_mockUnitOfWork.Object,_mockLogger.Object);

            _categories = new IQueryable<Category>()
            {
                new Category
                {
                    Id = new Guid("145fad2d-dca1-4a26-a41f-987db7847583"),
                    Name = "Notebook"
                },
                new Category
                {
                    Id = new Guid("1234a6ab-8ba5-4198-92c4-bd89af052f05"),
                    Name = "Smart Phone"
                },
                new Category
                {
                    Id = new Guid("1523dbfb-1b3d-45a0-90f7-463f7383e20c"),
                    Name = "Home Equipment"
                }
            };
        }

        [Fact]
        public async Task Get_GetAllCategories_Success()
        {
            //Generic Repository'i taklit ederek _category listesinin dönmesini sağladık.
            _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(_categories);

            var productWithCategoryDtos = new List<ProductWithCategoryDto>()
            {
                new ProductWithCategoryDto
                {
                    Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                    Name="Asus Zenbook",
                    Stock = 10,
                    Price = 40000,
                    Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
                    Picture = "asuszenbook.jpeg",
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








    }
}
