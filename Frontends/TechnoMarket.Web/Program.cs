using TechnoMarket.Web.Models;
using TechnoMarket.Web.Services;
using TechnoMarket.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Options pattern ile path'i okuyaca��z.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

var serviceApiSettings=builder.Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>();

//Catalog.Api i�in => HttpClient ile nesne t�reterek yeni �retti�imiz path �zerinden istek yapaca��z.
builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
});


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
