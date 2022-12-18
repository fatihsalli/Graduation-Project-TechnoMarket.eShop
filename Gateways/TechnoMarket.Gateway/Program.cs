using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Ocelot kütüphanesini ekledik.
builder.Services.AddOcelot();



var app = builder.Build();

//=> environment'a göre configuration dosyasýný verdik Ocelot için.
Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingcontext, config) =>
{
    config.AddJsonFile($"configuration.{hostingcontext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
})
    .ConfigureWebHostDefaults(webBuilder=>
    {
        webBuilder.UseStartup<Program>();
    });


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Asenkron olarak Ocelot'u middleware olarak ekledik.
await app.UseOcelot();

app.Run();
