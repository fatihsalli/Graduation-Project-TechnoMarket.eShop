using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region .Net6 Kullanýmý-Ocelot
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();

//Ocelot kütüphanesini ekledik.
builder.Services.AddOcelot(builder.Configuration);
#endregion

#region .Net5 Kullanýmý-Ocelot
//=> environment'a göre configuration dosyasýný verdik Ocelot için.
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
