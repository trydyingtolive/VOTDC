using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTDC.Models;

namespace VOTDC.Data
{
    public class DataContext : DbContext
    {
        public static IConfiguration Configuration { get; set; }

        public DataContext() : base() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Verse>().ToTable("Verses");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    IsAdmin = true
                }); ;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString
                );

            base.OnConfiguring(optionsBuilder);
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Verse> Verses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }
}
