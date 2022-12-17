using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingcontext, config) =>
{
    config.AddJsonFile($"configuration.{hostingcontext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});

//Ocelot kütüphanesini ekledik.
builder.Services.AddOcelot();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Asenkron olarak Ocelot'u middleware olarak ekledik.
await app.UseOcelot();

app.Run();
