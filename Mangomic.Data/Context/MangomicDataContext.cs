using Microsoft.EntityFrameworkCore;

namespace Mangomic.Data {
    public class MangomicDataContext : DbContext {
        public MangomicDataContext(DbContextOptions<MangomicDataContext> options) : base(options) {
        }
    }
}
