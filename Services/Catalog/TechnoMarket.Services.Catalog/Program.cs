using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Reflection;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Filters;
using TechnoMarket.Services.Catalog.Validations;
using TechnoMarket.Shared.Extensions;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    //NotFoundFilter => Generic oldu�u i�in bu �ekilde belirtik.
    builder.Services.AddScoped(typeof(NotFoundFilter<>));

    //Database
    builder.Services.AddDbContext<CatalogDbContext>(x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
        {
            option.MigrationsAssembly(Assembly.GetAssembly(typeof(CatalogDbContext)).GetName().Name);
        });
    });

    //AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
    //Service
    //builder.Services.AddScoped<IProductService, ProductService>();
    //builder.Services.AddScoped<ICategoryService, CategoryService>();

    builder.Services.AddControllers();

    //Fluent Validation ekledik.
    builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>());

    //Shared Library �zerinden d�nen response model yerine kendi modelimizi d�nd�k.
    builder.Services.UseCustomValidationResponseModel();

    #region Fluent Validation Response Model
    //Shared Library �zerinden yapt�k
    //FluentValidation ile d�nen response'u pasif hale getirip kendi response modelimizi d�nd�k.
    //builder.Services.Configure<ApiBehaviorOptions>(opt =>
    //{
    //    opt.SuppressModelStateInvalidFilter = true;
    //}); 
    #endregion

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


