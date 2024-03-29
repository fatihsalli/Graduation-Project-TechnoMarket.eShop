using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;
using TechnoMarket.Services.Order.Consumers;
using TechnoMarket.Services.Order.Data;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Services;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Services.Order.Settings;
using TechnoMarket.Services.Order.Settings.Interfaces;
using TechnoMarket.Services.Order.Validations;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;


var builder = WebApplication.CreateBuilder(args);

//=> Shared �zerinden ula�arak gerekli d�zenlemeleri yap�yoruz. Olduk�a temiz bir yakla��m.
builder.Host.UseSerilog(SeriLogger.Configure);

//Autofac kullanabiliriz ya da bir extension yazarak buray� bo�altabiliriz.

//Event yakalamak i�in => RabbitMQ ile
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

        //Command i okumak i�in Basket.Api taraf�nda olu�turdu�umuz kuyru�u dinliyoruz.
        cfg.ReceiveEndpoint("create-order-service", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });

        //Eventi yakalamak i�in kuyru�u consumer taraf�nda olu�turuyoruz.
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

