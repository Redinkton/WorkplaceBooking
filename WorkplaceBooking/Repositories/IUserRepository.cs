using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public interface IUserRepository
    {
        Task RegisterUserIfNotExistsAsync(string email, string name, string userId);
        Task<bool> IsAdmin(string userId);
        Task<UserProfile> GetUserProfileById (string userId);
        Task<UserProfile> GetUserProfileByEmail(string userId);
        Task<List<UserProfile>> GetUsers();
    }
}
