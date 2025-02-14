using System.Collections.ObjectModel;
using WorkplaceBooking.Models;
using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class SeatService
    {
        private readonly ISeatRepository _seatRepository;
        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<bool> ReserveSeatAsync(int seatNumber, string userId)
        {
            if (await _seatRepository.IsSeatOccupiedAsync(seatNumber))
            {
                return false;
            }

            if (await _seatRepository.IsUserAlreadyBookedAsync(userId))
            {
                return false;
            }

            await _seatRepository.AddBookingAsync(userId, seatNumber);
            return true;
        }

        public async Task<bool> FreeSeatAsync(int seatNumber, string userId)
        {

            var booking = await _seatRepository.GetBookingAsync(seatNumber, userId);
            if (booking == null)
            {
                return false;
            }

            await _seatRepository.RemoveBookingAsync(userId, seatNumber);
            return true;
        }

        public async Task<IReadOnlyDictionary<int, string>> GetOccupiedSeatsAsync()
        {
            var bookings = await _seatRepository.GetAllBookingsAsync();

            var occupiedSeats = bookings
                .Where(b => b.SeatNumber.HasValue)
                .ToDictionary(b => b.SeatNumber.Value, b => b.Name); 

            return new ReadOnlyDictionary<int, string>(occupiedSeats);
        }

    }
}