using System.Collections.ObjectModel;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Services
{
    public interface ISeatService
    {
        Task<bool> ReserveSeatAsync(int seatNumber, string userId);
        Task<bool> FreeSeatAsync(int seatNumber, string userId);
        Task<IReadOnlyDictionary<int, string>> GetOccupiedSeatsAsync();
        Task<IReadOnlyDictionary<int, string>> GetAllSeatsAsync();
        Task<IEnumerable<UserProfile>> GetAllBookingsAsync();
    }
}
