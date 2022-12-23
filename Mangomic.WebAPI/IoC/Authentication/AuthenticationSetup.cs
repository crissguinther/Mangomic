using Mangomic.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mangomic.API.IoC.Authentication {
    public static class AuthenticationSetup {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions));
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
            var securityKey = new SymmetricSecurityKey(key);

            services.Configure<JwtOptions>(options => {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                options.Expiration = int.Parse(jwtOptions[nameof(JwtOptions.Expiration)] ?? "0");
            });

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            });

            var tokenValidationParams = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtConfiguration: Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtConfiguration: Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = tokenValidationParams);
        }
    }
}
