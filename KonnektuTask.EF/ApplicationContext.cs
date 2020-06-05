using System;
using System.Collections.Generic;
using KonnektuTask.Core;
using Microsoft.EntityFrameworkCore;

namespace KonnektuTask.EF
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public ApplicationContext()
        {
        }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=KonnektuDb;Username=postgres;Password=postgres");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.SecretKey).HasDefaultValueSql("md5(random()::text)");
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Source>().HasKey(s => s.Id);
            modelBuilder.Entity<Source>().Property(s => s.SecretKey).HasDefaultValueSql("md5(random()::text)");

            modelBuilder.Entity<Source>().HasData(new List<Source>()
            {
                new Source() {Id = Guid.NewGuid()},
                new Source() {Id = Guid.NewGuid()}
            });

            modelBuilder.Entity<File>().HasKey(f => f.Id);
        }
    }
}