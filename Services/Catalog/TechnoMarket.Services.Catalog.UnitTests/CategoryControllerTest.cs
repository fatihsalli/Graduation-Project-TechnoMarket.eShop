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
using TechnoMarket.Shared.Exceptions;
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

        [Theory]
        [InlineData("145fad2d-dca1-4a26-a41f-987db7847583")]
        [InlineData("1234a6ab-8ba5-4198-92c4-bd89af052f05")]
        public async Task GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var categoryDto = _categories.First(x => x.Id == id);

            //CategoryService'i taklit ettik.
            _mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(categoryDto);

            var result = await _categoriesController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCategory = Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockCategoryService.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.Equal(id, returnCategory.Data.Id);
            Assert.Equal(categoryDto.Name, returnCategory.Data.Name);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCategoryService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Category with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Category with id ({id}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_ActionExecute_ReturnSuccessResult()
        {
            var categoryCreateDto = new CategoryCreateDto()
            {
                Name = "Smart Phone Accessories"
            };

            var categoryDto = new CategoryDto()
            {
                Id = "4f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = categoryCreateDto.Name,
            };

            _mockCategoryService.Setup(x => x.AddAsync(categoryCreateDto)).ReturnsAsync(categoryDto);

            var result = await _categoriesController.Create(categoryCreateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnCategory = Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(createActionResult.Value);

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.AddAsync(categoryCreateDto), Times.Once);

            Assert.Equal(categoryCreateDto.Name, returnCategory.Data.Name);
        }

        [Fact]
        public async Task Update_ActionExecute_ReturnSuccessResult()
        {
            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = "145fad2d-dca1-4a26-a41f-987db7847583",
                Name = "Ultra Slim Notebook"
            };

            //CategoryService'i taklit ettik.
            _mockCategoryService.Setup(x => x.UpdateAsync(categoryUpdateDto));

            var result = await _categoriesController.Update(categoryUpdateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockCategoryService.Verify(x => x.UpdateAsync(categoryUpdateDto), Times.Once);
        }

        [Fact]
        public async Task Update_IdIsNotEqualCategory_ReturnNotFoundException()
        {
            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = "123fad2d-dca1-4a26-a41f-987db7847583",
                Name = "Ultra Slim Notebook"
            };

            _mockCategoryService.Setup(x => x.UpdateAsync(categoryUpdateDto)).Throws(new NotFoundException($"Category ({categoryUpdateDto.Id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.Update(categoryUpdateDto));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.UpdateAsync(categoryUpdateDto), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Category ({categoryUpdateDto.Id}) not found!", exception.Message);
        }

        [Theory]
        [InlineData("145fad2d-dca1-4a26-a41f-987db7847583")]
        public async Task Delete_ActionExecute_ReturnSuccessResult(string id)
        {
            var categoryDto = _categories.First(x => x.Id == id);

            _mockCategoryService.Setup(x => x.RemoveAsync(id));

            var result = await _categoriesController.Delete(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);

            _mockCategoryService.Verify(x => x.RemoveAsync(id), Times.Once);
        }

        [Theory]
        [InlineData("155ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task Delete_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCategoryService.Setup(x => x.RemoveAsync(id)).Throws(new NotFoundException($"Category with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.Delete(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.RemoveAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Category with id ({id}) didn't find in the database.", exception.Message);
        }
    }
}
