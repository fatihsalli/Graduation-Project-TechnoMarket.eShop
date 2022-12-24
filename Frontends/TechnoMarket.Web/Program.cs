using FreeCourse.Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using SharedLibrary.Extensions;
using System.Configuration;
using TechnoMarket.Shared.Configurations;
using TechnoMarket.Web.Extensions;
using TechnoMarket.Web.Models;

var builder = WebApplication.CreateBuilder(args);

//Options pattern ile path'i okuyacaðýz.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

//PhotoHelper DI Contanier a ekledik.
builder.Services.AddSingleton<PhotoHelper>();

//Extension metot => HttpClient ile ilgili servisler için (HttpClient üzerinden iletiþimi saðlayacaðýz.)
builder.Services.AddHttpClientServices(builder.Configuration);

//IdentityService üzerinden kullanabilmek için.
builder.Services.AddHttpContextAccessor();

//CustomTokenOption ile appsetting arasýndaki iliþkiyi kurduk. TokenOption içerisindeki bilgileri CustomTokenOption nesnesi ile türetebilmek için bu iliþkiyi kurduk. (Options pattern)
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

//DI container içerisinde direkt olarak nesne türettik. Token sistemi için.
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
//Extension metot - SharedLibraryde oluþturduðumuz. Token doðrulama için. Birden fazla Api olduðu için SharedLibrary'de extension metot oluþturduk.
builder.Services.AddCustomTokenAuth(tokenOptions);

//Cookie oluþturuyoruz. Þemayý verdik servis tarafýnda yazdýðýmýz.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    //Refresh token 60 gün olduðu için burada da 60 gün verdik.
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    //60 gün içinde giriþ yaptýðýnda süre uzasýn mý=> true dedik
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
