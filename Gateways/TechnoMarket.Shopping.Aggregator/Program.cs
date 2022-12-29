using TechnoMarket.Shopping.Aggregator.Services;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//HttpClient => Catalog
builder.Services.AddHttpClient<ICustomerService, CustomerService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CustomerUrl"]));

//HttpClient => Order
builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderUrl"]));

//HttpClient => Basket
builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]));

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
