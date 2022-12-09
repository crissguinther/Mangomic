using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebGallery.Context;

namespace WebGallery.API {
    class WebGalleryAPI {
        public static void Main(string[] args) {
            WebGallery.Utils.DotEnv.Config();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = "server=" + Environment.GetEnvironmentVariable("DB_HOST") +
                                     ";user=" + Environment.GetEnvironmentVariable("DB_USER") +
                                     ";password=" + Environment.GetEnvironmentVariable("DB_PASSWORD") +
                                     ";database=" + Environment.GetEnvironmentVariable("DB_DATABASE");

            builder.Services.AddDbContext<WebGalleryContext>(optionsBuilder =>
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
