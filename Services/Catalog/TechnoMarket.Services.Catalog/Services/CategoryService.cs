using AutoMapper;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper, IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<CategoryDto> GetAll()
        {
            var categories = _repository.GetAll();
            return _mapper.Map<List<CategoryDto>>(categories.ToList());
        }

        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Category with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var categoryCheck = await _repository.AnyAsync(x => x.Id == new Guid(categoryUpdateDto.Id));

            if (!categoryCheck)
            {
                _logger.LogError($"Category with id ({categoryUpdateDto.Id}) didn't find in the database.");
                throw new NotFoundException($"Category with id ({categoryUpdateDto.Id}) didn't find in the database.");
            }

            var categoryUpdate = _mapper.Map<Category>(categoryUpdateDto);
            _repository.Update(categoryUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Category with id ({id}) didn't find in the database.");
            }

            _repository.Remove(category);
            await _unitOfWork.CommitAsync();
        }


    }
}
