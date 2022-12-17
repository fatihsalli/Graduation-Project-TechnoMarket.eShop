using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Filters
{
    public class NotFoundCategoryForProductFilter : IAsyncActionFilter
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly ILogger<NotFoundCategoryForProductFilter> _logger;

        public NotFoundCategoryForProductFilter(IGenericRepository<Category> categoryRepository, ILogger<NotFoundCategoryForProductFilter> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var productDtoCheck = context.ActionArguments.Values.SingleOrDefault(x => x.GetType() == typeof(ProductCreateDto) || x.GetType() == typeof(ProductUpdateDto));

            if (productDtoCheck == null)
            {
                //Create veya Update metotu değildir, yoluna devam edebilir.
                await next.Invoke();
                return;
            }

            var categoryId = "";

            if (productDtoCheck.GetType() == typeof(ProductCreateDto))
            {
                var productDto = (ProductCreateDto)productDtoCheck;
                categoryId = productDto.CategoryId;
            }
            else
            {
                var productDto = (ProductUpdateDto)productDtoCheck;
                categoryId = productDto.CategoryId;
            }

            var anyEntity = await _categoryRepository.AnyAsync(x => x.Id == new Guid(categoryId));

            if (anyEntity)
            {
                //CategoryId geçerli yoluna devam edebilir.
                await next.Invoke();
                return;
            }

            //Bu aşamaya gelmesi halinde data yok demektir. Bu durumda CustomResponse modelimizi NotFoundObjectResult ile döndük ve loglama yaptık.
            _logger.LogError($"Category with id ({categoryId}) didn't find in the database.");
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>
                .Fail(404, $"{nameof(Category)} with id ({categoryId}) didn't find in the database."));
        }
    }
}
