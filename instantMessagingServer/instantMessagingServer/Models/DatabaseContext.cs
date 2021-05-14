using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace instantMessagingServer.Models
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration Configuration;

        public DbSet<Users> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<PublicKeys> PublicKeys { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Peers> Peers { get; set; }

        public DatabaseContext(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("sqlite"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(u => u.Id);
            modelBuilder.Entity<Users>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Users>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<Tokens>().HasKey(t => t.UserId);

            modelBuilder.Entity<PublicKeys>().HasKey(p => p.UserId);

            modelBuilder.Entity<Logs>().HasKey(l => l.Id);

            modelBuilder.Entity<Friends>().HasKey(f => f.UserId);

            modelBuilder.Entity<Peers>().HasKey(p => p.UserId);
        }
    }
}
