using Microsoft.Extensions.Options;
using TechnoMarket.Services.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);

//Options Pattern
builder.Services.Configure<CatalogDatabaseSettings>(builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));
builder.Services.AddSingleton<ICatalogDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
