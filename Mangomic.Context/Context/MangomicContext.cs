using Mangomic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mangomic.Context {
    public class MangomicContext : DbContext {
        public MangomicContext(DbContextOptions<MangomicContext> options) : base(options) { 
        }
        public DbSet<User> Users { get; set; }
    }
}
