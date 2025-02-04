using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Data;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _appDbContext;

        public SeatRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddBookingAsync(SeatBooking seatBooking)
        {
            await _appDbContext.SeatBookings.AddAsync(seatBooking);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveBookingAsync(SeatBooking seatBooking)
        {
            _appDbContext.SeatBookings.Remove(seatBooking);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<SeatBooking?> GetBookingAsync(int seatNumber, string userName)
        {
            return await _appDbContext.SeatBookings
                 .FirstOrDefaultAsync(s => s.SeatNumber == seatNumber && s.UserName == userName);
        }

        public async Task<bool> IsSeatOccupiedAsync(int seatNumber)
        {
            return await _appDbContext.SeatBookings.AnyAsync(s => s.SeatNumber == seatNumber);
        }

        public async Task<bool> IsUserAlreadyBookedAsync(string userName)
        {
            return await _appDbContext.SeatBookings.AnyAsync(s => s.UserName == userName);
        }

        public async Task<List<SeatBooking>> GetAllBookingsAsync()
        {
            return await _appDbContext.SeatBookings.ToListAsync();
        }
    }
}
