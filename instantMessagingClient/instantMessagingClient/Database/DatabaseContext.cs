using Microsoft.EntityFrameworkCore;

namespace instantMessagingClient.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MyMessages> MyMessages { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=instantMessaging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyMessages>().HasKey(u => u.Id);
            modelBuilder.Entity<MyMessages>().Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}
