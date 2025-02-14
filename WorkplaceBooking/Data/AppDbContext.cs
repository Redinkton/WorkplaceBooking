using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasIndex(s => s.SeatNumber)
                .IsUnique(); //Use unique index to avoid 'race conditions'.
        }
    }
}

