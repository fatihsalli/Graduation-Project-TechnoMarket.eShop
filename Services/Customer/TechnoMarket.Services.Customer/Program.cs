using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Repositories;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
using TechnoMarket.Services.Customer.Validations;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

//=> Shared üzerinden ulaþarak gerekli düzenlemeleri yapýyoruz. Oldukça temiz bir yaklaþým.
builder.Host.UseSerilog(SeriLogger.Configure);

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


