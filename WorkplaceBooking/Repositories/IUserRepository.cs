using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public interface IUserRepository
    {
        public Task RegisterUserIfNotExistsAsync(string email, string name, string userId);
        public Task<bool> IsAdmin(string userId);
        public Task<UserProfile> GetUserProfileById (string userId);
        public Task<UserProfile> GetUserProfileByEmail(string userId);
    }
}
