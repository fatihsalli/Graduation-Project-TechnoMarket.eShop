using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.AuthServer.Data;
using TechnoMarket.AuthServer.Models;
using TechnoMarket.AuthServer.Services;
using TechnoMarket.AuthServer.Services.Interfaces;
using TechnoMarket.AuthServer.Settings;
using TechnoMarket.Shared.Configurations;
using TechnoMarket.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

//DI Register
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Database Ba�lant�
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

//�yelik Sistemi
builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//CustomTokenOption ile appsetting aras�ndaki ili�kiyi kurduk. TokenOption i�erisindeki bilgileri CustomTokenOption nesnesi ile t�retebilmek i�in bu ili�kiyi kurduk. (Options pattern)
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

//Client ile appsetting aras�ndaki ili�kiyi kurduk. Clients i�erisindeki bilgileri CustomTokenOption nesnesi ile t�retebilmek i�in bu ili�kiyi kurduk. (Options pattern)
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

//Tokenlar ile ilgili (Do�rulama i�lemi i�in)
builder.Services.AddAuthentication(options =>
{
    //Farkl� login giri�i sistemleri (�rne�in bir sitede bayi ve m��teri giri�i gibi) olsayd� �emalar� ay�rmam�z gerekirdi. Ama bizde tek login giri�i sistemi oldu�u i�in ay�rmad�k. 
    //options.DefaultAuthenticateScheme = "Bearer"; da yazabilirdik ancak onun yerine default bir �ema verdik.

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //"AddJwtBearer(JwtBearerDefaults.AuthenticationScheme" bu yazan �ema ile �stteki �emay� ba�lamak i�in alttaki kodu yazd�k.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //Json web token kulland���m�z� bu �ekilde belirttik. Ayn� �emay� vererek options ile i�eri girdik.
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    //DI container i�erisinde direkt olarak nesne t�rettik. Token sistemi i�in.
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    //Token ile ilgili detaylar� veriyoruz.appsettingste yazd���m�z.
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //Valid k�sm�nda datay� veriyoruz. Validate k�sm�nda kontrol ediyoruz.
        ValidIssuer = tokenOptions.Issuer,
        //"www.authserver.com" var m� yok mu diye kontrol ettik. Di�erlerini her MiniApp kendi i�inde kontrol edecek.
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        //Kontrol - Validate yapt���m�z k�s�m buras�
        //�mza do�rulama
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        //Bir tokena �m�r verdi�imizde 1 saat �rne�in, default olarak 5 dkl�k pay ekler. Farkl� zonelardaki farkl� serverlar aras�ndaki farktan dolay� defaul olarak 5 dk ekler. A�a��da biz bu �zelli�i kapatt�k. (Farkl� serverlara kurulan apiler aras�ndaki zaman fark�n� tolere etmek i�in ) (Postmanden test yapaca��m�z i�in kapatt�k)
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
