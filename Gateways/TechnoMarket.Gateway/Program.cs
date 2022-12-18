using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Ocelot k�t�phanesini ekledik.
builder.Services.AddOcelot();



var app = builder.Build();

//=> environment'a g�re configuration dosyas�n� verdik Ocelot i�in.
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