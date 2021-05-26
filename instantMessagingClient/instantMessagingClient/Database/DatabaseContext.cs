using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instantMessagingCore.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace instantMessagingClient.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<myMessages> myMessages { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=instantMessaging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<myMessages>().HasKey(u => u.Id);
            modelBuilder.Entity<myMessages>().Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}
