using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using TechnoMarket.Shared.Configurations;
using TechnoMarket.Shared.Services;

namespace SharedLibrary.Extensions
{
    //Json web token doğrulamasına diğer apilerde ulaşabilmek adına ortak bir extension class oluşturduk. Buradan Apilere hizmet edecek."AuthServer.API" projesi startup içerisinde token doğrulama ile ilgili yazdığımız kodları buraya kopyaladık.
    public static class UseCustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection services, CustomTokenOption tokenOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }



    }
}
