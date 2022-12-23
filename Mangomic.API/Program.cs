using Mangomic.API.IoC;
using Mangomic.API.IoC.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mangomic.WebAPI {
    class Mangomic {
        public static void Main(string[] args) {
            WebGallery.Utils.DotEnv.Config();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            InjectorConfig.RegisterServices(builder.Services);
            AuthenticationSetup.ConfigureAuthentication(builder.Services, builder.Configuration);

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
