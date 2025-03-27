using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Seat)
                .WithOne(s => s.UserProfile)
                .HasForeignKey<UserProfile>(up => up.SeatId);

            var seats = Enumerable.Range(1, 31)
                .Select(i => new Seat
                {
                    Id = i,
                    Number = i
                })
                .ToArray();

            modelBuilder.Entity<Seat>().HasData(seats);
        }
    }
}

