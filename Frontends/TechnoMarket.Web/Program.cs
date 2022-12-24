using FreeCourse.Web.Helpers;
using TechnoMarket.Web.Extensions;
using TechnoMarket.Web.Models;

var builder = WebApplication.CreateBuilder(args);

//Options pattern ile path'i okuyacaðýz.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

//PhotoHelper DI Contanier a ekledik.
builder.Services.AddSingleton<PhotoHelper>();

//Extension metot => HttpClient ile ilgili servisler için (HttpClient üzerinden iletiþimi saðlayacaðýz.)
builder.Services.AddHttpClientServices(builder.Configuration);

//Cookie => User bilgileri için
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    x.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    x.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
    {
        Name = "Login_Cookie"
    };
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(10);
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
