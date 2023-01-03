using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;
using TechnoMarket.Services.Basket.Services;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Services.Basket.Settings;
using TechnoMarket.Services.Basket.Validations;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;


var builder = WebApplication.CreateBuilder(args);

//=> Shared �zerinden ula�arak gerekli d�zenlemeleri yap�yoruz. Olduk�a temiz bir yakla��m.
builder.Host.UseSerilog(SeriLogger.Configure);

//MassTransit => RabbitMQ ile command g�ndermek i�in
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

#region AddMassTransitHostedService() Gerek Kalmad�
//Buna gerek kalmad� son versiyon ile. StackOverFlow => https://stackoverflow.com/questions/70187422/addmasstransithostedservice-cannot-be-found
//builder.Services.AddMassTransitHostedService(); 
#endregion

//Options Pattern
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));

#region RedisService Connection
//RedisService nesnesini options pattern kullanarak host,port bilgilerini verip connect metodunu tetikleyerek ba�lant�y� sa�lad�k. 
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

//Shared Library �zerinden d�nen response model yerine kendi modelimizi d�nd�k.
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




