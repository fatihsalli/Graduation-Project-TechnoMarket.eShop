using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.AuthServer.Data;
using TechnoMarket.AuthServer.Models;

var builder = WebApplication.CreateBuilder(args);

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Database Baðlantý
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

//Üyelik Sistemi
builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail= true;
    options.Password.RequireNonAlphanumeric= false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



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
