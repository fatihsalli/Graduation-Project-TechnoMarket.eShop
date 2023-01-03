using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Filters;
using TechnoMarket.Services.Catalog.Repositories;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Services.Catalog.Validations;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;


var builder = WebApplication.CreateBuilder(args);

//=> Shared üzerinden ulaþarak gerekli düzenlemeleri yapýyoruz. Oldukça temiz bir yaklaþým.
builder.Host.UseSerilog(SeriLogger.Configure);

//NotFoundFilter => Generic olduðu için bu þekilde belirtik.
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddScoped<NotFoundCategoryForProductFilter>();

//Event fýrlatmak için => RabbitMQ ile
builder.Services.AddMassTransit(x =>
{
    //Dafault port:5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

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
//Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

//Fluent Validation ekledik.
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>());

//Shared Library üzerinden dönen response model yerine kendi modelimizi döndük.
builder.Services.UseCustomValidationResponseModel();

#region Fluent Validation Response Model - Shared Library'e Taþýndý
//Shared Library üzerinden yaptýk
//FluentValidation ile dönen response'u pasif hale getirip kendi response modelimizi döndük.
//builder.Services.Configure<ApiBehaviorOptions>(opt =>
//{
//    opt.SuppressModelStateInvalidFilter = true;
//}); 
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Otomatik Update-Database
//Migrationlarý database'e otomatik yansýtmak ve data basmak için. Dikkat migration oluþturmuyor mevcut migration'ý database tarafýnda güncel deðilse güncelliyor. 
#endregion
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
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



