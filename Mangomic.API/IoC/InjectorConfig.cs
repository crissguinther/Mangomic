﻿using Mangomic.Application.Models;
using Mangomic.Data;
using Mangomic.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mangomic.API.IoC {
    public static class InjectorConfig {
        public static void RegisterServices(this IServiceCollection services) {
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

            services.AddSingleton<JwtOptions>();
            #endregion
        }
    }
}