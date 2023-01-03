using Serilog;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

//=> Shared �zerinden ula�arak gerekli d�zenlemeleri yap�yoruz. Olduk�a temiz bir yakla��m.
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

//wwwroot olu�turduktan sonra bu klas�r� d�� d�nyaya a�mak i�in.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
