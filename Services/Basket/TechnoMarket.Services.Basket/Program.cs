using Microsoft.Extensions.Options;
using TechnoMarket.Services.Basket.Services;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Services.Basket.Settings;

var builder = WebApplication.CreateBuilder(args);

//Options Pattern
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));

#region RedisService Connection
//RedisService nesnesini options pattern kullanarak host,port bilgilerini verip connect metodunu tetikleyerek baðlantýyý saðladýk. 
#endregion
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings=sp.GetService<IOptions<RedisSettings>>().Value;
    var redis=new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});

//Service
builder.Services.AddScoped<IBasketService,BasketService>();

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
