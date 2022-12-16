using Microsoft.Extensions.Options;
using TechnoMarket.Services.Basket.Settings;
using TechnoMarket.Services.Basket.Settings.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Options Pattern
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));
builder.Services.AddSingleton<IRedisSettings>(sp => sp.GetRequiredService<IOptions<RedisSettings>>().Value);

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
