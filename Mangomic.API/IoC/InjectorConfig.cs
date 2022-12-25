using Mangomic.API.IoC.Authentication;
using Mangomic.Application.Interfaces.Services;
using Mangomic.Application.Models;
using Mangomic.Domain.Context;
using Mangomic.Identity.Context;
using Mangomic.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mangomic.API.IoC {
    public static class InjectorConfig {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration) {
            #region ConnectionString
            string connectionString = "server=" + Environment.GetEnvironmentVariable("DB_HOST") +
                                     ";user=" + Environment.GetEnvironmentVariable("DB_USER") +
                                     ";password=" + Environment.GetEnvironmentVariable("DB_PASSWORD") +
                                     ";database=" + Environment.GetEnvironmentVariable("DB_DATABASE");
            #endregion   

            #region Databases
            services.AddDbContext<MangomicDataContext>(optionsBuilder =>
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddDbContext<IdentityDataContext>(optionsBuilder =>
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion

            #region Services
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<JwtOptions>();
            services.AddScoped<IIdentityService, IdentityService>();


            AuthenticationSetup.ConfigureAuthentication(services, configuration);
            #endregion
        }
    }
}
