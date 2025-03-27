using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Data;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public class SeatRepository(AppDbContext appDbContext, IUserRepository userRepository) : ISeatRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task AddBookingAsync(string userId, int seatNumber)
        {
            var userProfile = await _userRepository.GetUserProfileById(userId) ?? throw new InvalidOperationException("Пользователь не найден");
            var seat = await _appDbContext.Seats.FirstOrDefaultAsync(s => s.Number == seatNumber);
            if (seat == null)
            {
                throw new InvalidOperationException("Место не найдено");
            }
            userProfile.Seat = seat;
            seat.UserProfile = userProfile;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveBookingAsync(string userId, int seatNumber)
        {
            var userProfile = await _userRepository.GetUserProfileById(userId) ?? throw new InvalidOperationException("Пользователь не найден");
            var seat = await _appDbContext.Seats
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(s => s.Number == seatNumber);
            if (seat != null)
            {
                userProfile.Seat = null;
                seat.UserProfile = null;
            }
            else if(seat == null)
            {
                throw new InvalidOperationException("Место не найдено");
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveBookingAsync(int seatNumber)
        {
            var seat = await _appDbContext.Seats.FirstOrDefaultAsync(s => s.Number == seatNumber);
            if (seat != null && seat.UserProfile != null)
            {
                seat.UserProfile = null;
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<UserProfile?> GetBookingAsync(int seatNumber, string userId)
        {
            return await _appDbContext.UserProfiles
                 .Include(u => u.Seat)
                 .FirstOrDefaultAsync(u => u.Id == userId && u.Seat != null && u.Seat.Number == seatNumber);
        }

        public async Task<bool> IsSeatOccupiedAsync(int seatNumber)
        {
            var seat = await _appDbContext.Seats
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(s => s.Number == seatNumber);
            return seat?.UserProfile != null;
        }

        public async Task<bool> IsUserAlreadyBookedAsync(string userId)
        {

            var user = await _appDbContext.UserProfiles.FindAsync(userId);
            return user != null && user.SeatId != null;
        }

        public async Task<List<UserProfile>> GetAllBookingsAsync()
        {
            return await _appDbContext.UserProfiles
                .Where(u => u.SeatId != null)
                .Include(u => u.Seat)
                .ToListAsync();
        }

        public async Task<UserProfile?> GetBookingBySeatNumberAsync(int seatNumber)
        {
            var seat = await _appDbContext.Seats
                        .Include(s => s.UserProfile)
                        .FirstOrDefaultAsync(s => s.Number == seatNumber);
            return seat?.UserProfile;
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            return await _appDbContext.Seats.Include(u => u.UserProfile).ToListAsync();
        }
    }
}
