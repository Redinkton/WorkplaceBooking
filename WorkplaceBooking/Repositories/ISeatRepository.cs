using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public interface ISeatRepository
    {
        public Task AddBookingAsync(string userId, int seatNumber);
        public Task RemoveBookingAsync(string userId, int seatNumber);
        public Task<UserProfile?> GetBookingAsync(int seatNumber, string userId);
        public Task<bool> IsSeatOccupiedAsync(int seatNumber);
        public Task<bool> IsUserAlreadyBookedAsync(string userId);
        public Task<List<UserProfile>> GetAllBookingsAsync();
    }
}