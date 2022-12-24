using FreeCourse.Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using SharedLibrary.Extensions;
using System.Configuration;
using TechnoMarket.Shared.Configurations;
using TechnoMarket.Web.Extensions;
using TechnoMarket.Web.Models;

var builder = WebApplication.CreateBuilder(args);

//Options pattern ile path'i okuyaca��z.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

//PhotoHelper DI Contanier a ekledik.
builder.Services.AddSingleton<PhotoHelper>();

//Extension metot => HttpClient ile ilgili servisler i�in (HttpClient �zerinden ileti�imi sa�layaca��z.)
builder.Services.AddHttpClientServices(builder.Configuration);

//IdentityService �zerinden kullanabilmek i�in.
builder.Services.AddHttpContextAccessor();

//CustomTokenOption ile appsetting aras�ndaki ili�kiyi kurduk. TokenOption i�erisindeki bilgileri CustomTokenOption nesnesi ile t�retebilmek i�in bu ili�kiyi kurduk. (Options pattern)
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

//DI container i�erisinde direkt olarak nesne t�rettik. Token sistemi i�in.
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
//Extension metot - SharedLibraryde olu�turdu�umuz. Token do�rulama i�in. Birden fazla Api oldu�u i�in SharedLibrary'de extension metot olu�turduk.
builder.Services.AddCustomTokenAuth(tokenOptions);

//Cookie olu�turuyoruz. �emay� verdik servis taraf�nda yazd���m�z.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    //Refresh token 60 g�n oldu�u i�in burada da 60 g�n verdik.
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    //60 g�n i�inde giri� yapt���nda s�re uzas�n m�=> true dedik
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "udemywebcookie";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //Area Route
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
        );
    });

    //Confirmation Route
    endpoints.MapControllerRoute(
        name: "confirmation",
        pattern: "{controller=Home}/{action=Confirmation}/{id}/{code}");

    //Default Route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
