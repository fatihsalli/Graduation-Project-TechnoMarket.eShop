using Serilog;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shopping.Aggregator.Services;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//=> Shared üzerinden ulaþarak gerekli düzenlemeleri yapýyoruz. Oldukça temiz bir yaklaþým.
builder.Host.UseSerilog(SeriLogger.Configure);

//=> Loglamamýmýz handle etmek için oluþturduðumuz classýmýzý ekliyoruz.
builder.Services.AddTransient<LoggingDelegatingHandler>();

//HttpClient => Catalog
builder.Services.AddHttpClient<ICustomerService, CustomerService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CustomerUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>();

//HttpClient => Order
builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>();

//HttpClient => Basket
builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
