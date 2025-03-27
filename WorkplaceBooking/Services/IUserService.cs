using System.Security.Claims;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(ClaimsPrincipal user);
        Task<bool> IsAdminAsync(string userId);
        Task<IEnumerable<UserProfile>> GetUsersAsync();
    }
}
