using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Filters
{
    public class NotFoundCategoryForProductFilter : IAsyncActionFilter
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public NotFoundCategoryForProductFilter(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var objectDictionary = context.ActionArguments.Values;

            foreach (var item in objectDictionary)
            {
                
            }

            var categoryId = "";

            if (categoryId.IsNullOrEmpty())
            {
                await next.Invoke();
                return;
            }

            var anyEntity = await _categoryRepository.AnyAsync(x => x.Id == new Guid(categoryId));

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            //Burada data yok olarak yani Category Not found olarak response döndük.
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>
                .Fail(404, $"{nameof(Category)} with id ({categoryId}) didn't find in the database."));

        }
    }
}
