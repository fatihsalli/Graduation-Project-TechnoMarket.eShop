using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : class
    {
        //Exceptionlarımız global olarak yazıldı. Filter neden yazıyoruz? Herhangi bir entity için data=null olduğunda ek business yapılması gerekebilir. Örneğin mesaj kuyruğa gidip mesaj atsın gibi veya kullanıcıya email atmak gibi.

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public NotFoundFilter(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Gelen id'yi yakaladık.
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            //Id null ise demekki GetById veya benzeri bir metot kullanılmamış demektir. Next.Invoke() ile yoluna devam edebilir ve return ile metottan çıkarak.
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            //Cast işlemi yaptık
            var id = (string)idValue;
            object anyEntity = null;

            if (typeof(T).Name == nameof(Category))
            {
                anyEntity = await _categoryService.GetByIdAsync(id);
            }
            else if (typeof(T).Name == nameof(Product))
            {
                anyEntity = await _productService.GetByIdAsync(id);
            }

            //Yani data var ise yine yoluna devam edecek
            if (anyEntity != null)
            {
                await next.Invoke();
                return;
            }
            //Burada data yok olarak yani Not found olarak response döndük.
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"Product ({id}) not found"));
        }






    }
}
