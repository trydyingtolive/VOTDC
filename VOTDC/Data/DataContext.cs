using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTDC.Models;

namespace VOTDC.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@example.com",
                    Password = "",
                    IsAdmin = true
                }); ;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Verse> Verses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }
}
