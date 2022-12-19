using Microsoft.Extensions.Configuration;
using TechnoMarket.Web.Extensions;
using TechnoMarket.Web.Models;
using TechnoMarket.Web.Services;
using TechnoMarket.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Options pattern ile path'i okuyacaðýz.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

//Extension metot => HttpClient ile ilgili servisler için (HttpClient üzerinden iletiþimi saðlayacaðýz.)
builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
