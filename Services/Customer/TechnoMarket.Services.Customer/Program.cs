using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Customer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





//Database
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),b=> b.MigrationsAssembly("")));
builder.Services.AddScoped<DbContext>(provider => provider.GetService<CustomerContext>());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
