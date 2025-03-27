using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class AdminService(IUserRepository userRepository, ISeatRepository seatRepository) : IAdminService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISeatRepository _seatRepository = seatRepository;

        public async Task<bool> AdminReserveSeat(string email, int seatNumber)
        {
            var userProfile = await _userRepository.GetUserProfileByEmail(email);
            if (userProfile == null)
                return false;

            if (userProfile.Seat != null)
            {
                await _seatRepository.RemoveBookingAsync(userProfile.Id, userProfile.Seat.Number);
            }

            var currentBooking = await _seatRepository.GetBookingBySeatNumberAsync(seatNumber);
            if (currentBooking != null)
            {
                await _seatRepository.RemoveBookingAsync(seatNumber);
            }

            await _seatRepository.AddBookingAsync(userProfile.Id, seatNumber);
            return true;
        }


        public async Task<bool> AdminCancelBooking(string email)
        {
            var userProfile = await _userRepository.GetUserProfileByEmail(email);
            if (userProfile != null && userProfile.Seat != null && userProfile.Seat.Number != 0)
            {
                await _seatRepository.RemoveBookingAsync(userProfile.Id, userProfile.Seat.Number);
                return true;
            }

            return false;
        }

        public async Task<bool> AdminCancelBooking(int seatNumber)
        {
            if (!await _seatRepository.IsSeatOccupiedAsync(seatNumber))
            {
                return false;
            }

            await _seatRepository.RemoveBookingAsync(seatNumber);
            return true;
        }

        public async Task<bool> AdminCheckUserBooking(string userEmail)
        {
            var userProfile = await _userRepository.GetUserProfileByEmail(userEmail);
            if (userProfile?.Seat == null || userProfile.Seat.Number == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AdminCheckSeat(int seatNumber)
        {
            return await _seatRepository.IsSeatOccupiedAsync(seatNumber);
        }
    }
}
