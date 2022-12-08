using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;
using TechnoMarket.Services.Customer.Repositories;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services;
using TechnoMarket.Services.Customer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Repository => Generic
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//Service => Generic
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

//Database
builder.Services.AddDbContext<CustomerDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostreSql"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(CustomerDbContext)).GetName().Name);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
