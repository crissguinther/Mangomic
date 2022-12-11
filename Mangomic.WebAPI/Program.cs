using Mangomic.Context;
using Mangomic.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mangomic.WebAPI {
    class Mangomic {
        public static void Main(string[] args) {
            WebGallery.Utils.DotEnv.Config();
            var builder = WebApplication.CreateBuilder(args);

            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = "server=" + Environment.GetEnvironmentVariable("DB_HOST") +
                                     ";user=" + Environment.GetEnvironmentVariable("DB_USER") +
                                     ";password=" + Environment.GetEnvironmentVariable("DB_PASSWORD") +
                                     ";database=" + Environment.GetEnvironmentVariable("DB_DATABASE");

            builder.Services.AddSingleton<ITokenService, TokenService>();

            builder.Services.AddDbContext<MangomicContext>(optionsBuilder =>
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }                      

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{version=v1}/{controller}/{action}/{id?}"
            );

            app.Run();
        }
    }
}
