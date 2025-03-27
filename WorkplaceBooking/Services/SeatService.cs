using System.Collections.ObjectModel;
using WorkplaceBooking.Models;
using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class SeatService(ISeatRepository seatRepository) : ISeatService
    {
        private readonly ISeatRepository _seatRepository = seatRepository;

        public async Task<bool> ReserveSeatAsync(int seatNumber, string userId)
        {
            if (await _seatRepository.IsSeatOccupiedAsync(seatNumber))
            {
                return false;
            }

            if (await _seatRepository.IsUserAlreadyBookedAsync(userId))
            {
                FreeSeatAsync(seatNumber, userId);
            }

            try
            {
                await _seatRepository.AddBookingAsync(userId, seatNumber);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ReserveSeatAsync: {ex.Message}");
                return false;
            }
            
        }

        public async Task<bool> FreeSeatAsync(int seatNumber, string userId)
        {

            var booking = await _seatRepository.GetBookingAsync(seatNumber, userId);
            if (booking == null)
            {
                return false;
            }

            try
            {
                await _seatRepository.RemoveBookingAsync(userId, seatNumber);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FreeSeatAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<IReadOnlyDictionary<int, string>> GetOccupiedSeatsAsync()
        {
            var usersWithSeats = await _seatRepository.GetAllBookingsAsync();

            var occupiedSeats = usersWithSeats
                .Where(u => u.Seat != null)
                .ToDictionary(u => u.Seat.Number, u => u.Name);

            return new ReadOnlyDictionary<int, string>(occupiedSeats);
        }

        public async Task<IReadOnlyDictionary<int, string>> GetAllSeatsAsync()
        {
            var users = await _seatRepository.GetAllSeatsAsync();

            var freeSeats = users
                .ToDictionary(s => s.Number, u => u.UserProfile?.Name);

            return new ReadOnlyDictionary<int, string>(freeSeats);
        }

        public async Task<IEnumerable<UserProfile>> GetAllBookingsAsync()
        {
            return await _seatRepository.GetAllBookingsAsync();
        }
    }
}