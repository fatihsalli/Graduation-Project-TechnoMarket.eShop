using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Repositories;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
using TechnoMarket.Services.Customer.Validations;
using TechnoMarket.Shared.Extensions;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Repository Pattern => Generic
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

    //Service => Customer
    builder.Services.AddScoped<ICustomerService, CustomerService>();

    //UnitOfWork Design Pattern
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    //AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));

    //Database
    builder.Services.AddDbContext<CustomerDbContext>(x =>
    {
        x.UseNpgsql(builder.Configuration.GetConnectionString("PostreSql"), option =>
        {
            option.MigrationsAssembly(Assembly.GetAssembly(typeof(CustomerDbContext)).GetName().Name);
        });
    });

    builder.Services.AddControllers();

    //Fluent Validation ekledik.
    builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CustomerCreateDtoValidator>());

    //Shared Library üzerinden dönen response model yerine kendi modelimizi döndük.
    builder.Services.UseCustomValidationResponseModel();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    //Migrationlarý database'e otomatik yansýtmak ve data basmak için
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
        context.Database.Migrate();

        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        if (!service.GetAllAsync().Result.Any())
        {
            service.AddAsync(new CustomerCreateDto
            {
                FirstName = "Fatih",
                LastName = "Þallý",
                Email = "mimsallifatih@gmail.com",
                Address = new AddressDto()
                {
                    AddressLine = "Kadýköy",
                    City = "Ýstanbul",
                    Country = "Türkiye",
                    CityCode = 34
                }
            }).Wait();
        }
    }

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


