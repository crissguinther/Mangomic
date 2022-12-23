
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mangomic.Identity.Context {
    public class IdentityDataContext : IdentityDbContext {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }
    }
}