using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Middlewares;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.Settings;
using TechnoMarket.Services.Catalog.Settings.Interfaces;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Options Pattern
    builder.Services.Configure<CatalogDatabaseSettings>(builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));
    builder.Services.AddSingleton<ICatalogDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

    


    //Database
    builder.Services.AddScoped<ICatalogContext, CatalogContext>();
    //AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
    //Service
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();

    //FluentValidation => Filter ile ekledik.
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    #region CategorySeedData
    //CategorySeed Data => Data.CatalogContextSeed i�erisinde yazd�k bu da program.cs i�erisinde yazman�n alternatifi.
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
    #endregion

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


