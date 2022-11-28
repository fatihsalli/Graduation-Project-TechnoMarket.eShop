using Microsoft.Extensions.Options;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Middlewares;
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
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//CategorySeed Data => CatalogContextSeed içerisinde hallettik bu da alternatif.
//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

//    if (!categoryService.GetAllAsync().Result.Data.Any())
//    {
//        categoryService.CreateAsync(new CategoryCreateDto { Name = "Notebook" });
//        categoryService.CreateAsync(new CategoryCreateDto { Name = "Smart Phone" });
//    }
//}


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
