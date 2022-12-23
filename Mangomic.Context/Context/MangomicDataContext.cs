using Microsoft.EntityFrameworkCore;

namespace Mangomic.Context {
    public class MangomicDataContext : DbContext {
        public MangomicDataContext(DbContextOptions<MangomicDataContext> options) : base(options) {
        }
    }
}
