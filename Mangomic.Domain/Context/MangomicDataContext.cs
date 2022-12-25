using Microsoft.EntityFrameworkCore;

namespace Mangomic.Domain.Context {
    public class MangomicDataContext : DbContext {
        public MangomicDataContext(DbContextOptions options) : base(options) {}
    }
}
