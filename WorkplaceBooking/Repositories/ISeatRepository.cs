using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public interface ISeatRepository
    {
        public Task AddBookingAsync(SeatBooking seatBooking);
        public Task RemoveBookingAsync(SeatBooking seatBooking);
        public Task<SeatBooking?> GetBookingAsync(int seatNumber, string userName);

        public Task<bool> IsSeatOccupiedAsync(int seatNumber);
        public Task<bool> IsUserAlreadyBookedAsync(string userName);
        public Task<List<SeatBooking>> GetAllBookingsAsync();
    }
}