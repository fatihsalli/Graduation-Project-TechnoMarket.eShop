using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : class
    {
        //Exceptionlarımız global olarak yazıldı. Filter neden yazıyoruz? Herhangi bir entity için data=null olduğunda ek business yapılması gerekebilir. Örneğin mesaj kuyruğa gidip mesaj atsın gibi veya kullanıcıya email atmak gibi.
        private readonly IGenericRepository<T> _repository;
        private readonly ILogger<NotFoundFilter<T>> _logger;

        public NotFoundFilter(IGenericRepository<T> repository, ILogger<NotFoundFilter<T>> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            var anyEntity = await _repository.GetByIdAsync(id);

            //Yani data var ise yine yoluna devam edecek
            if (anyEntity != null)
            {
                await next.Invoke();
                return;
            }
            //Burada data yok olarak yani Not found olarak response döndük.
            _logger.LogError($"{typeof(T).Name} with id ({id}) didn't find in the database.");
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>
                .Fail(404, $"{typeof(T).Name} with id ({id}) didn't find in the database."));
        }
    }
}
