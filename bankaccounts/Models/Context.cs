using Microsoft.EntityFrameworkCore;

namespace bankaccounts.Models {
    public class Context : DbContext {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transcation> Transcations { get; set; }
    }    
}