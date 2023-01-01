using FreeCourse.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Web.Data;
using TechnoMarket.Web.Extensions;
using TechnoMarket.Web.Models;

var builder = WebApplication.CreateBuilder(args);


//Options pattern ile path'i okuyacaðýz.
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));

//PhotoHelper DI Contanier a ekledik.
builder.Services.AddSingleton<PhotoHelper>();

//Database
builder.Services.AddDbContext<UserContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

//Identity (kimlik yönetimi) dahil etme
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

//Cookie
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    x.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    x.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
    {
        Name = "Login_Cookie"
    };
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});


//Extension metot => HttpClient ile ilgili servisler için (HttpClient üzerinden iletiþimi saðlayacaðýz.)
builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Otomatik Update-Database
//Migrationlarý database'e otomatik yansýtmak ve data basmak için. Dikkat migration oluþturmuyor mevcut migration'ý database tarafýnda güncel deðilse güncelliyor. 
#endregion
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserContext>();
    context.Database.Migrate();
}

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
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
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
