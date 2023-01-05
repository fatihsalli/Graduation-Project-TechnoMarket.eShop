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
        private List<Category> _categories;

        public CategoryServiceTest()
        {
            _mockRepo = new Mock<IGenericRepository<Category>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryService>>();
            _categoryService = new CategoryService(_mockMapper.Object,_mockRepo.Object,_mockUnitOfWork.Object,_mockLogger.Object);

            _categories = new List<Category>()
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
        public void Get_GetAllCategories_Success()
        {
            //Generic Repository'i taklit ederek _category listesinin dönmesini sağladık.
            _mockRepo.Setup(x => x.GetAll()).Returns(_categories.AsQueryable());

            var categoryDtos = new List<CategoryDto>()
            {
                new CategoryDto
                {
                    Id = "145fad2d-dca1-4a26-a41f-987db7847583",
                    Name = "Notebook"
                },
                new CategoryDto
                {
                    Id = "1234a6ab-8ba5-4198-92c4-bd89af052f05",
                    Name = "Smart Phone"
                },
                new CategoryDto
                {
                    Id = "1523dbfb-1b3d-45a0-90f7-463f7383e20c",
                    Name = "Home Equipment"
                }
            };

            //Mapper'ı taklit ederek List<ProductWithCategoryDto> oluşturduk
            _mockMapper.Setup(x => x.Map<List<CategoryDto>>(_categories)).Returns(categoryDtos);

            var result = _categoryService.GetAll();

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockRepo.Verify(x => x.GetAll(), Times.Once);
            _mockMapper.Verify(x => x.Map<List<CategoryDto>>(_categories), Times.Once);

            //Result tipini kontrol ediyoruz.
            Assert.IsAssignableFrom<List<CategoryDto>>(result);

            //Mapper ile gönderdiğimiz data ile resulttan gelen datayı check ediyoruz.
            Assert.Equal(categoryDtos, result);

            //Bir tane ürün eklediğimiz için bir tane olup olmadığını kontrol ediyoruz. Birden fazla olması durumunda "Assert.Equal<int>(2, result.Count);" olarak kullanabiliriz.
            Assert.Equal<int>(3,result.Count);
        }








    }
}
