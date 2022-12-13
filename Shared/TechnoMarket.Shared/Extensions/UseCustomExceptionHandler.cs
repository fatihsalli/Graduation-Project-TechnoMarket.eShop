using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Shared.Extensions
{
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
                    var message = $"Error Message:{exceptionFeature.Error.Message} | Inner Exception: {exceptionFeature.Error.InnerException.Message}";
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, message);
                    //Custom middleware oluşturduğumuz için kendimiz json formatına serialize etmemiz gerekir.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
