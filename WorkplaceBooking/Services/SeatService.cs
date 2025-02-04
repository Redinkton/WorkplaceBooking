using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Data;
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

        public async Task<bool> ReserveSeatAsync(int seatNumber, string userName)
        {
            if (await _seatRepository.IsSeatOccupiedAsync(seatNumber))
            {
                return false;
            }

            if (await _seatRepository.IsUserAlreadyBookedAsync(userName))
            {
                return false;
            }

            var booking = new SeatBooking { SeatNumber = seatNumber, UserName = userName };
            await _seatRepository.AddBookingAsync(booking);
            return true;
        }

        public async Task<bool> FreeSeatAsync(int seatNumber, string userName)
        {

            var booking = await _seatRepository.GetBookingAsync(seatNumber, userName);
            if (booking == null)
            {
                return false;
            }

            await _seatRepository.RemoveBookingAsync(booking);
            return true;
        }

        public async Task<IReadOnlyDictionary<int, string>> GetOccupiedSeatsAsync()
        {
            var bookings = await _seatRepository.GetAllBookingsAsync();
            return bookings.ToDictionary(b => b.SeatNumber, b => b.UserName);
        }
    }
}