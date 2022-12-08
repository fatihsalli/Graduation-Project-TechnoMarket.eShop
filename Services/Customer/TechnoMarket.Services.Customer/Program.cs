using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;

var builder = WebApplication.CreateBuilder(args);



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
