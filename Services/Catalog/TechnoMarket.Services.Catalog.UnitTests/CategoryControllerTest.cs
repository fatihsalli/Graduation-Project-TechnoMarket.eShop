using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Services.Catalog.Controllers;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoriesController _categoriesController;
        //Fake data
        private List<CategoryDto> _categories;

        public CategoryControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _categoriesController = new CategoriesController(_mockCategoryService.Object);

            _categories = new List<CategoryDto>()
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
        }

        [Fact]
        public void GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockCategoryService.Setup(x => x.GetAll()).Returns(_categories);

            var result = _categoriesController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCategories = Assert.IsAssignableFrom<CustomResponseDto<List<CategoryDto>>>(createActionResult.Value);

            Assert.Equal<int>(3, returnCategories.Data.Count);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockCategoryService.Verify(x => x.GetAll(), Times.Once);
        }





    }
}
