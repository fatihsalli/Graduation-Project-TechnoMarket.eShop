using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using TechnoMarket.Shared.CommonLogging;

var builder = WebApplication.CreateBuilder(args);

//=> Shared �zerinden ula�arak gerekli d�zenlemeleri yap�yoruz. Olduk�a temiz bir yakla��m.
builder.Host.UseSerilog(SeriLogger.Configure);

//.Net6 Kullan�m�-Ocelot
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();

//Ocelot k�t�phanesini ekledik.
builder.Services.AddOcelot(builder.Configuration);

#region .Net5 Kullan�m�-Ocelot
//=> environment'a g�re configuration dosyas�n� verdik Ocelot i�in.
//Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingcontext, config) =>
//{
//    config.AddJsonFile($"configuration.{hostingcontext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
//})
//    .ConfigureWebHostDefaults(webBuilder =>
//    {
//        webBuilder.UseStartup<Program>();
//    }); 
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Asenkron olarak Ocelot'u middleware olarak ekledik.
await app.UseOcelot();

app.Run();
