using System.Security.Claims;
using WorkplaceBooking.Repositories;

namespace WorkplaceBooking.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
    }
}
