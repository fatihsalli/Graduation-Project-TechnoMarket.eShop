using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using TechnoMarket.Services.Order.Consumers;
using TechnoMarket.Services.Order.Data;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Services;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Services.Order.Settings;
using TechnoMarket.Services.Order.Settings.Interfaces;
using TechnoMarket.Services.Order.Validations;
using TechnoMarket.Shared.Extensions;
using TechnoMarket.Shared.Messages;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Autofac kullanabiliriz ya da bir extension yazarak burayý boþaltabiliriz.

    //Event yakalamak için => RabbitMQ ile
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<ProductNameChangedEventConsumer>();
        x.AddConsumer<CreateOrderMessageCommandConsumer>();

        //Dafault port:5672
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
            {
                host.Username("guest");
                host.Password("guest");
            });

            //Command i okumak için Basket.Api tarafýnda oluþturduðumuz kuyruðu dinliyoruz.
            cfg.ReceiveEndpoint("create-order-service", e =>
            {
                e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
            });

            //Eventi yakalamak için kuyruðu consumer tarafýnda oluþturuyoruz.
            cfg.ReceiveEndpoint("product-name-changed-event-order-service", e =>
            {
                e.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
            });
        });
    });

    //Options Pattern
    builder.Services.Configure<OrderDatabaseSettings>(builder.Configuration.GetSection(nameof(OrderDatabaseSettings)));
    builder.Services.AddSingleton<IOrderDatabaseSettings>(sp => sp.GetRequiredService<IOptions<OrderDatabaseSettings>>().Value);

    //Database
    builder.Services.AddScoped<IOrderContext, OrderContext>();
    //AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
    //Service
    builder.Services.AddScoped<IOrderService, OrderService>();

    builder.Services.AddControllers();

    //Fluent Validation ekledik.
    builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<OrderCreateDtoValidator>());

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




