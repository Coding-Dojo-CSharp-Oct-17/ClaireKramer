using Microsoft.EntityFrameworkCore;

namespace restauranter.Models {
    public class Context : DbContext {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Review> Review { get; set; }
    }
}