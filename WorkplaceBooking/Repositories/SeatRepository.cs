using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Data;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;

        public SeatRepository(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }
        public async Task AddBookingAsync(string userId, int seatNumber)
        {
            var userProfile = await _userRepository.GetUserProfileById(userId) ?? throw new InvalidOperationException("Пользователь не найден");
            userProfile.SeatNumber = seatNumber;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveBookingAsync(string userId, int seatNumber)
        {
            var userProfile = await _userRepository.GetUserProfileById(userId) ?? throw new InvalidOperationException("Пользователь не найден");
            userProfile.SeatNumber = null;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<UserProfile?> GetBookingAsync(int seatNumber, string userId)
        {
            return await _appDbContext.UserProfiles
                 .FirstOrDefaultAsync(u => u.SeatNumber == seatNumber && u.Id == userId);
        }

        public async Task<bool> IsSeatOccupiedAsync(int seatNumber)
        {
            return await _appDbContext.UserProfiles.AnyAsync(u => u.SeatNumber == seatNumber);
        }

        public async Task<bool> IsUserAlreadyBookedAsync(string userName)
        {
            return await _appDbContext.UserProfiles.AnyAsync(u => u.Name == userName);
        }

        public async Task<List<UserProfile>> GetAllBookingsAsync()
        {
            return await _appDbContext.UserProfiles.ToListAsync();
        }
    }
}
