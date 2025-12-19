using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ↔ Trip (1:N)
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Trips)
                        .WithOne(t => t.User)
                        .HasForeignKey(t => t.UserID);

            // Country ↔ City (1:N)
            modelBuilder.Entity<Country>()
                        .HasMany(c => c.Cities)
                        .WithOne(c => c.Country)
                        .HasForeignKey(c => c.CountryId);

            // Trip ↔ Country and City (FK)
            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.Country)
                        .WithMany()
                        .HasForeignKey(t => t.CountryId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.City)
                        .WithMany()
                        .HasForeignKey(t => t.CityId)
                        .OnDelete(DeleteBehavior.Restrict);

            // User ↔ Country and City (FK)
            modelBuilder.Entity<User>()
                        .HasOne(u => u.Country)
                        .WithMany()
                        .HasForeignKey(u => u.CountryId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                        .HasOne(u => u.City)
                        .WithMany()
                        .HasForeignKey(u => u.CityId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
