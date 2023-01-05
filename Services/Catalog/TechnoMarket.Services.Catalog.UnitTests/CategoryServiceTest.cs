using AutoMapper;
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
            _categoryService = new CategoryService(_mockMapper.Object, _mockRepo.Object, _mockUnitOfWork.Object, _mockLogger.Object);

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

            //Mapper'ı taklit ederek List<CategoryDto> oluşturduk
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
            Assert.Equal<int>(3, result.Count);
        }

        [Theory]
        [InlineData("145fad2d-dca1-4a26-a41f-987db7847583")]
        public async Task GetById_GetCategory_Success(string id)
        {
            //Kendi oluşturduğumuz listten ilgili id'yi bulduk.
            var category = _categories.First(x => x.Id == new Guid(id));

            //Category Repository'i taklit ederek geriye bu product'ı döndük.
            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);

            var categoryDto = new CategoryDto
            {
                Id = category.Id.ToString(),
                Name = category.Name,
            };

            //Mapper'ı taklit ederek "CategoryDto" oluşturduk.
            _mockMapper.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

            var result = await _categoryService.GetByIdAsync(id);

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            _mockMapper.Verify(x => x.Map<CategoryDto>(category), Times.Once);
            Assert.Equal(category.Id.ToString(), result.Id);
            Assert.IsAssignableFrom<CategoryDto>(result);
            Assert.Equal(categoryDto, result);
        }

        [Theory]
        [InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        public async Task GetById_IdNotFound_ReturnException(string id)
        {
            Category category = null;

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoryService.GetByIdAsync(id));

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Category with id ({id}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_CreateCategory_Success()
        {
            var categoryCreateDto = new CategoryCreateDto()
            {
                Name = "Smart Phone Accessories",                
            };

            var category = new Category()
            {
                //Id = new Guid("3f7ca3fc-e45b-4857-9950-2ff2a8e5977d"),
                Name = categoryCreateDto.Name,
            };

            _mockMapper.Setup(x => x.Map<Category>(categoryCreateDto)).Returns(category);

            var categoryDto = new CategoryDto()
            {
                Id = "3f7ca3fc-e45b-4857-9950-2ff2a8e5977d",
                Name = categoryCreateDto.Name,
            };

            _mockRepo.Setup(x => x.AddAsync(category));
            _mockUnitOfWork.Setup(x => x.CommitAsync());
            _mockMapper.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

            var result = await _categoryService.AddAsync(categoryCreateDto);

            _mockRepo.Verify(x => x.AddAsync(category), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<CategoryDto>(category), Times.Once);
            _mockMapper.Verify(x => x.Map<Category>(categoryCreateDto), Times.Once);
            Assert.IsAssignableFrom<CategoryDto>(result);
            Assert.Equal(categoryDto, result);
            Assert.Equal(categoryCreateDto.Name, result.Name);
        }

        [Fact]
        public async Task Update_UpdateCategory_Success()
        {
            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = "145fad2d-dca1-4a26-a41f-987db7847583",
                Name = "Slim Notebook"
            };

            var category = _categories.Where(x => x.Id == new Guid(categoryUpdateDto.Id)).SingleOrDefault();

            category.Name = categoryUpdateDto.Name;

            _mockMapper.Setup(x => x.Map<Category>(categoryUpdateDto)).Returns(category);

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(categoryUpdateDto.Id))).ReturnsAsync(true);
            _mockRepo.Setup(x => x.Update(category)).Returns(category);
            _mockUnitOfWork.Setup(x => x.CommitAsync());

            await _categoryService.UpdateAsync(categoryUpdateDto);

            _mockRepo.Verify(x => x.Update(category), Times.Once);
            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(categoryUpdateDto.Id)), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<Category>(categoryUpdateDto));
            Assert.Equal(_categories.First().Name, categoryUpdateDto.Name);
        }

        [Fact]
        public async Task Update_IdNotFound_ReturnException()
        {
            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = "204fad2d-dca1-4a26-a41f-987db7847583",
                Name = "Slim Notebook"
            };

            _mockRepo.Setup(x => x.AnyAsync(x => x.Id == new Guid(categoryUpdateDto.Id))).ReturnsAsync(false);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoryService.UpdateAsync(categoryUpdateDto));

            _mockRepo.Verify(x => x.AnyAsync(x => x.Id == new Guid(categoryUpdateDto.Id)), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Category with id ({categoryUpdateDto.Id}) didn't find in the database.", exception.Message);
        }

        [Theory]
        [InlineData("145fad2d-dca1-4a26-a41f-987db7847583")]
        public async Task Remove_RemoveCategory_Success(string id)
        {
            var category = _categories.First(x => x.Id == new Guid(id));

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);
            _mockRepo.Setup(x => x.Remove(category));

            await _categoryService.RemoveAsync(id);

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
            _mockRepo.Verify(x => x.Remove(category), Times.Once);
        }

        [Theory]
        [InlineData("401ca3fc-e45b-4857-9950-2ff2a8e5977d")] //FakeId
        public async Task Remove_IdNotFound_ReturnException(string id)
        {
            Category category = null;

            _mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _categoryService.RemoveAsync(id));

            _mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal($"Category with id ({id}) didn't find in the database.", exception.Message);
        }

    }
}
