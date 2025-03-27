using System.Security.Claims;
using WorkplaceBooking.Models;
using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task RegisterUserAsync(ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!string.IsNullOrEmpty(name) &&  !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(email))
            {
                await _userRepository.RegisterUserIfNotExistsAsync(email, name, id);
            }
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            return await _userRepository.IsAdmin(userId);
        }

        public async Task<IEnumerable<UserProfile>> GetUsersAsync()
        {
            return await _userRepository.GetUsers();
        }
    }
}
