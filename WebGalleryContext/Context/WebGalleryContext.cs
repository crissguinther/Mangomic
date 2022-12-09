using Microsoft.EntityFrameworkCore;

namespace WebGallery.Context {
    internal class WebGalleryContext : DbContext {
        public WebGalleryContext(DbContextOptions<WebGalleryContext> options) : base(options) { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL($"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                $"UserID={Environment.GetEnvironmentVariable("DB_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")}");
        }
    }
}
