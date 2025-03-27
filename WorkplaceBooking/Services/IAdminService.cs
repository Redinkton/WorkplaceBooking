namespace WorkplaceBooking.Services
{
    public interface IAdminService
    {

        Task<bool> AdminReserveSeat(string email, int seatNumber);
        
        Task<bool> AdminCancelBooking(string email);
        Task<bool> AdminCancelBooking(int seatNumber);
        Task<bool> AdminCheckUserBooking(string userEmail);
        Task<bool> AdminCheckSeat(int seatNumber);
    }
}
