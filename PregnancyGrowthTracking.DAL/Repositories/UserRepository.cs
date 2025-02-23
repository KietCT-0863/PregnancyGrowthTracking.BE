using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public UserRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                .Include(u => u.Role) 
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users
                .Include(u => u.Role) 
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }


        public async Task<User> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) return false;

            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> UpdateUserProfileImageAsync(int userId, string imageUrl)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return false;

            user.ProfileImageUrl = imageUrl;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string?> GetUserProfileImageAsync(int userId)
        {
            // Lấy URL ảnh từ cột ProfileImageUrl
            var user = await _dbContext.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.ProfileImageUrl)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> DeleteUserProfileImageAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return false;

            // Xóa URL trong ProfileImageUrl
            user.ProfileImageUrl = null;

            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> SearchUsersByKeywordAsync(string keyword)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .Where(u => !string.IsNullOrEmpty(u.FullName) &&
                            EF.Functions.Like(u.FullName, $"%{keyword}%"))
                .ToListAsync();
        }


    }
}
