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

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CategoryServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IGenericRepository<Category>> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<CategoryService>> _mockLogger;        
        private readonly ICategoryService _categoryService;
        private List<Category> _category;

        public CategoryServiceTest()
        {
            _mockRepo = new Mock<IGenericRepository<Category>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryService>>();
            _categoryService = new CategoryService(_mockMapper.Object,_mockRepo.Object,_mockUnitOfWork.Object,_mockLogger.Object);

            _category = new List<Category>()
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
        






    }
}
