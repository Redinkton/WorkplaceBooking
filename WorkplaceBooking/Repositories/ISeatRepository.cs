using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public interface ISeatRepository
    {
        Task AddBookingAsync(string userId, int seatNumber);
        Task RemoveBookingAsync(string userId, int seatNumber);
        Task RemoveBookingAsync(int seatNumber);
        Task<UserProfile?> GetBookingAsync(int seatNumber, string userId);
        Task<bool> IsSeatOccupiedAsync(int seatNumber);
        Task<bool> IsUserAlreadyBookedAsync(string userId);
        Task<List<UserProfile>> GetAllBookingsAsync();
        Task<UserProfile?> GetBookingBySeatNumberAsync(int seatNumber);
        Task<List<Seat>> GetAllSeatsAsync();
    }
}