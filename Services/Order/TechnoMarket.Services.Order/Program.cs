using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechnoMarket.Services.Order.Data;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Filters;
using TechnoMarket.Services.Order.Services;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Services.Order.Settings;
using TechnoMarket.Services.Order.Settings.Interfaces;
using TechnoMarket.Services.Order.Validations;
using TechnoMarket.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


//Autofac kullanabiliriz ya da bir extension yazarak burayý boþaltabiliriz.

//Options Pattern
builder.Services.Configure<OrderDatabaseSettings>(builder.Configuration.GetSection(nameof(OrderDatabaseSettings)));
builder.Services.AddSingleton<IOrderDatabaseSettings>(sp => sp.GetRequiredService<IOptions<OrderDatabaseSettings>>().Value);

//Database
builder.Services.AddScoped<IOrderContext, OrderContext>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Service
builder.Services.AddScoped<IOrderService, OrderService>();

//FluentValidation => Filter ile ekledik.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<OrderCreateDtoValidator>());

//FluentValidation ile dönen response'u pasif hale getirip kendi response modelimizi döndük.
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

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
