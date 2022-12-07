using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Shared.Extensions
{
    public static class UseCustomValidationResponse
    {
        //Burada yaptığımız işlem FluentValidationda hata fırlattığında hazır gönderilen response modeli göndermeyip biz bir model oluşturarak onu göndermektir.
        public static void UseCustomValidationResponseModel(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);

                    var response = CustomResponseDto<NoContentDto>.Fail(400, errors.ToList());

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
