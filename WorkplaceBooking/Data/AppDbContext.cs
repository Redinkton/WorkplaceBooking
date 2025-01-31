using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SeatBooking> SeatBookings { get; set; }
    }
}

