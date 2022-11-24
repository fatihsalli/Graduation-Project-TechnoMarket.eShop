using Microsoft.Extensions.Options;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.Settings;
using TechnoMarket.Services.Catalog.Settings.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Options Pattern
builder.Services.Configure<CatalogDatabaseSettings>(builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));
builder.Services.AddSingleton<ICatalogDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

//Database
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
//DI Container Scope
builder.Services.AddScoped<IProductService, ProductService>();

//CategorySeed Data



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
