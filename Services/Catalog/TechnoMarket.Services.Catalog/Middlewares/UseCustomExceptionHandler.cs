using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using TechnoMarket.Services.Catalog.Exceptions;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Middlewares
{
    //TODO: Kontrol edilecek araya girip bizim Response'u dönmek istiyoruz.
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //Sonlandırıcı middleware
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                    //Custom middleware oluşturduğumuz için kendimiz json formatına serialize etmemiz gerekir.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
