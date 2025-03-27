using Microsoft.EntityFrameworkCore;
using WorkplaceBooking.Data;
using WorkplaceBooking.Models;

namespace WorkplaceBooking.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task RegisterUserIfNotExistsAsync(string email, string name, string userId)
        {
            var existingUser = await _appDbContext.UserProfiles.FindAsync(userId);
            if (existingUser == null)
            {
                var newUser = new UserProfile
                {
                    Email = email,
                    Name = name,
                    Id = userId,
                    IsAdmin = false,
                };

                await _appDbContext.UserProfiles.AddAsync(newUser);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAdmin(string userId)
        {
            var userProfile = await _appDbContext.UserProfiles.FindAsync(userId);
            if (userProfile == null)
            {
                return false;
            }
            return userProfile.IsAdmin;
        }

        public async Task<UserProfile> GetUserProfileById(string userId)
        {
            var userProfile = await _appDbContext.UserProfiles.FindAsync(userId);
            if (userProfile == null)
            {
                throw new InvalidOperationException("Пользователь не найден");
            }
            return userProfile;
        }

        public async Task<UserProfile> GetUserProfileByEmail(string email)
        {
            var userProfile = await _appDbContext.UserProfiles
                .Include(u => u.Seat)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (userProfile == null)
            {
                throw new InvalidOperationException("Пользователь не найден");
            }
            return userProfile;
        }

        public async Task<List<UserProfile>> GetUsers()
        {
            return await _appDbContext.UserProfiles
                .Include(u => u.Seat)
                .ToListAsync();
        }
    }
}
