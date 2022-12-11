using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.Repositories;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Repository Pattern => Generic
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//Service => Customer
builder.Services.AddScoped<ICustomerService, CustomerService>();

//UnitOfWork Design Pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Database
builder.Services.AddDbContext<CustomerDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostreSql"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(CustomerDbContext)).GetName().Name);
    });
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Migrationlar� database'e otomatik yans�tmak i�in
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    context.Database.Migrate();
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
