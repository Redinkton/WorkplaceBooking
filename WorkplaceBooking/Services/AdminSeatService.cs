using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class AdminSeatService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;

        public AdminSeatService(IUserRepository userRepository, ISeatRepository seatRepository)
        {
            _userRepository = userRepository;
            _seatRepository = seatRepository;
        }

        public async Task<bool> AdminReserveSeat(string email, int seatNumber, string adminId)
        {
            if (await _userRepository.IsAdmin(adminId))
            {
                var userProfile = await _userRepository.GetUserProfileByEmail(email);
                if (userProfile != null && !await _seatRepository.IsSeatOccupiedAsync(seatNumber))
                {
                    userProfile.SeatNumber = seatNumber;
                    await _seatRepository.AddBookingAsync(userProfile.Id, seatNumber);
                    return true; 
                }
            }
            return false; 
        }

        public async Task<bool> AdminCancelBooking(string email, int seatNumber, string adminId)
        {
            if (await _userRepository.IsAdmin(adminId))
            {
                var userProfile = await _userRepository.GetUserProfileByEmail(email);
                if (userProfile != null && userProfile.SeatNumber == seatNumber) 
                {
                    userProfile.SeatNumber = null; 
                    await _seatRepository.RemoveBookingAsync(userProfile.Id, seatNumber);
                    return true; 
                }
            }
            return false;
        }
    }
}
