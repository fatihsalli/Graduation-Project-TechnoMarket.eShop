using Serilog;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

//=> Shared üzerinden ulaþarak gerekli düzenlemeleri yapýyoruz. Oldukça temiz bir yaklaþým.
builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Custom middleware
app.UseCustomException();

//wwwroot oluþturduktan sonra bu klasörü dýþ dünyaya açmak için.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
