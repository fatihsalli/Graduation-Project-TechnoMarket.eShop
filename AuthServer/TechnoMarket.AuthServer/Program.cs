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

//Database Baðlantý
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

//Üyelik Sistemi
builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//CustomTokenOption ile appsetting arasýndaki iliþkiyi kurduk. TokenOption içerisindeki bilgileri CustomTokenOption nesnesi ile türetebilmek için bu iliþkiyi kurduk. (Options pattern)
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

//Client ile appsetting arasýndaki iliþkiyi kurduk. Clients içerisindeki bilgileri CustomTokenOption nesnesi ile türetebilmek için bu iliþkiyi kurduk. (Options pattern)
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

//Tokenlar ile ilgili (Doðrulama iþlemi için)
builder.Services.AddAuthentication(options =>
{
    //Farklý login giriþi sistemleri (Örneðin bir sitede bayi ve müþteri giriþi gibi) olsaydý þemalarý ayýrmamýz gerekirdi. Ama bizde tek login giriþi sistemi olduðu için ayýrmadýk. 
    //options.DefaultAuthenticateScheme = "Bearer"; da yazabilirdik ancak onun yerine default bir þema verdik.

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //"AddJwtBearer(JwtBearerDefaults.AuthenticationScheme" bu yazan þema ile üstteki þemayý baðlamak için alttaki kodu yazdýk.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //Json web token kullandýðýmýzý bu þekilde belirttik. Ayný þemayý vererek options ile içeri girdik.
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    //DI container içerisinde direkt olarak nesne türettik. Token sistemi için.
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    //Token ile ilgili detaylarý veriyoruz.appsettingste yazdýðýmýz.
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //Valid kýsmýnda datayý veriyoruz. Validate kýsmýnda kontrol ediyoruz.
        ValidIssuer = tokenOptions.Issuer,
        //"www.authserver.com" var mý yok mu diye kontrol ettik. Diðerlerini her MiniApp kendi içinde kontrol edecek.
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        //Kontrol - Validate yaptýðýmýz kýsým burasý
        //Ýmza doðrulama
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        //Bir tokena ömür verdiðimizde 1 saat örneðin, default olarak 5 dklýk pay ekler. Farklý zonelardaki farklý serverlar arasýndaki farktan dolayý defaul olarak 5 dk ekler. Aþaðýda biz bu özelliði kapattýk. (Farklý serverlara kurulan apiler arasýndaki zaman farkýný tolere etmek için ) (Postmanden test yapacaðýmýz için kapattýk)
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
