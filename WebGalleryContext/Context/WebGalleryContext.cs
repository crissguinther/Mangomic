using WebGallery.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebGallery.Context {
    public class WebGalleryContext : DbContext {
        public WebGalleryContext(DbContextOptions<WebGalleryContext> options) : base(options) { 
        }
        public DbSet<User> Users { get; set; }
    }
}
