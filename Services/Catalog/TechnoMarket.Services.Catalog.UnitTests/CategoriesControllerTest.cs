using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnoMarket.Services.Catalog.Controllers;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoriesController _categoriesController;

        //Fake data
        private List<CategoryDto> _categories;

        public CategoriesControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _categoriesController = new CategoriesController(_mockCategoryService.Object);

            _categories = new List<CategoryDto>()
            {
                new CategoryDto()
                {
                    Id="455f191e810c19729de860po",
                    Name="Smart Phone"
                },
                new CategoryDto()
                {
                    Id="456f191e810c19729de860ea",
                    Name="Notebook"
                }
            };
        }

        [Fact]
        public async void GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockCategoryService.Setup(x => x.GetAllAsync()).ReturnsAsync(CustomResponseDto<List<CategoryDto>>.Success(200, _categories));

            var result = await _categoriesController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnProducts = Assert.IsAssignableFrom<CustomResponseDto<List<CategoryDto>>>(createActionResult.Value);

            Assert.Equal(2, returnProducts.Data.Count);
        }

        [Theory]
        [InlineData("455f191e810c19729de860po")]
        [InlineData("456f191e810c19729de860ea")]
        public async void GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var categoryDto = _categories.First(x => x.Id == id);

            _mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CustomResponseDto<CategoryDto>.Success(200, categoryDto));

            var result = await _categoriesController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCategories = Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(createActionResult.Value);

            Assert.Equal(id, returnCategories.Data.Id);
            Assert.Equal(categoryDto.Name, returnCategories.Data.Name);

        }

        [Theory]
        [InlineData("500f191e810c19729de860fa")] //Invalid Id
        public async void GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCategoryService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Category ({id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Category ({id}) not found!", exception.Message);
        }

        [Fact]
        public async void Create_ActionExecute_ReturnSuccessResult()
        {
            var categoryCreateDto = new CategoryCreateDto()
            {
                Name = "Home Electronics",
            };

            var categoryDto = new CategoryDto()
            {
                Id = "457f191e810c19729de860ea",
                Name = categoryCreateDto.Name
            };

            _mockCategoryService.Setup(x => x.CreateAsync(categoryCreateDto)).ReturnsAsync(CustomResponseDto<CategoryDto>.Success(201, categoryDto));

            var result = await _categoriesController.Create(categoryCreateDto);

            _mockCategoryService.Verify(x => x.CreateAsync(categoryCreateDto), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnCategory = Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(createActionResult.Value);

            Assert.Equal(categoryCreateDto.Name, returnCategory.Data.Name);
        }

        [Fact]
        public async void Update_ActionExecute_ReturnSuccessResult()
        {
            var categorytDto = _categories.First();
            var categoryUpdateDto = new CategoryUpdateDto
            {
                Id = categorytDto.Id,
                //Senaryo=> İsmini güncelledik
                Name = "Kitchen Electronics"
            };
            var categoryToReturn = categorytDto;
            categoryToReturn.Name = categoryUpdateDto.Name;

            //CategoryService'i taklit ettik.
            _mockCategoryService.Setup(x => x.UpdateAsync(categoryUpdateDto)).ReturnsAsync(CustomResponseDto<CategoryDto>.Success(200, categoryToReturn));

            var result = await _categoriesController.Update(categoryUpdateDto);

            _mockCategoryService.Verify(x => x.UpdateAsync(categoryUpdateDto), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnCategory = Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(createActionResult.Value);

            Assert.Equal(categoryUpdateDto.Id, returnCategory.Data.Id);
            Assert.Equal(categoryUpdateDto.Name, returnCategory.Data.Name);
        }

        [Fact]
        public async void Update_IdIsNotEqualProduct_ReturnNotFoundException()
        {
            var categorytDto = _categories.First();
            var categoryUpdateDto = new CategoryUpdateDto
            {
                Id = "501f191e810c19729de860ea",
                //Invalid Id
                Name = categorytDto.Name
            };

            _mockCategoryService.Setup(x => x.UpdateAsync(categoryUpdateDto)).Throws(new NotFoundException($"Category ({categoryUpdateDto.Id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.Update(categoryUpdateDto));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.UpdateAsync(categoryUpdateDto), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Category ({categoryUpdateDto.Id}) not found!", exception.Message);
        }

        [Theory]
        [InlineData("455f191e810c19729de860po")]
        [InlineData("456f191e810c19729de860ea")]
        public async void Delete_ActionExecute_ReturnSuccessResult(string id)
        {
            var categoryDto = _categories.First(x => x.Id == id);

            _mockCategoryService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(CustomResponseDto<NoContentDto>.Success(204));

            var result = await _categoriesController.Delete(id);

            _mockCategoryService.Verify(x => x.DeleteAsync(id), Times.Once);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, createActionResult.StatusCode);
        }

        [Theory]
        [InlineData("501f191e810c19729de860ab")] //Wrong id
        public async void Delete_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockCategoryService.Setup(x => x.DeleteAsync(id)).Throws(new NotFoundException($"Category ({id}) not found!"));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoriesController.Delete(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockCategoryService.Verify(x => x.DeleteAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Category ({id}) not found!", exception.Message);
        }




    }
}
