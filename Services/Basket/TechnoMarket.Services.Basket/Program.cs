using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using TechnoMarket.Services.Basket.Services;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Services.Basket.Settings;
using TechnoMarket.Services.Basket.Validations;
using TechnoMarket.Shared.Extensions;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //MassTransit => RabbitMQ ile command göndermek için
    builder.Services.AddMassTransit(x =>
    {
        // Default port :5672
        x.UsingRabbitMq((context, config) =>
        {
            config.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
            {
                //Default olarak username ve password guest olarak gelmektedir.
                host.Username("guest");
                host.Password("guest");
            });
        });
    });

    #region AddMassTransitHostedService() Gerek Kalmadý
    //Buna gerek kalmadý son versiyon ile. StackOverFlow => https://stackoverflow.com/questions/70187422/addmasstransithostedservice-cannot-be-found
    //builder.Services.AddMassTransitHostedService(); 
    #endregion

    //AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));

    //Options Pattern
    builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));

    #region RedisService Connection
    //RedisService nesnesini options pattern kullanarak host,port bilgilerini verip connect metodunu tetikleyerek baðlantýyý saðladýk. 
    #endregion
    builder.Services.AddSingleton<RedisService>(sp =>
    {
        var redisSettings = sp.GetService<IOptions<RedisSettings>>().Value;
        var redis = new RedisService(redisSettings.Host, redisSettings.Port);
        redis.Connect();
        return redis;
    });

    //Service
    builder.Services.AddScoped<IBasketService, BasketService>();

    builder.Services.AddControllers();

    //Fluent Validation ekledik.
    builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<BasketDtoValidator>());

    //Shared Library üzerinden dönen response model yerine kendi modelimizi döndük.
    builder.Services.UseCustomValidationResponseModel();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //Custom middleware
    app.UseCustomException();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}



