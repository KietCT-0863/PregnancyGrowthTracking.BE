using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class FoetusRepository : IFoetusRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public FoetusRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Foetus> CreateFoetusAsync(Foetus foetus)
        {
            _dbContext.Foetus.Add(foetus);
            await _dbContext.SaveChangesAsync();
            return foetus;
        }

        public async Task<bool> IsFoetusNameExistsAsync(int userId, string name)
        {
            return await _dbContext.Foetus
                .AnyAsync(f => f.UserId == userId && f.Name == name);
        }

        public async Task<IEnumerable<Foetus>> GetFoetusesByUserIdAsync(int userId)
        {
            return await _dbContext.Foetus
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Foetus?> GetFoetusByIdAsync(int foetusId)
        {
            return await _dbContext.Foetus
                .FirstOrDefaultAsync(f => f.FoetusId == foetusId);
        }

        public async Task<bool> DeleteFoetusAsync(int foetusId, int userId)
        {
            // ✅ Kiểm tra xem thai nhi có thuộc về người dùng không
            var foetus = await _dbContext.Foetus
                .Include(f => f.GrowthData)
                .FirstOrDefaultAsync(f => f.FoetusId == foetusId && f.UserId == userId);

            if (foetus == null) return false;

            // ✅ Xóa dữ liệu phát triển trước
            _dbContext.GrowthData.RemoveRange(foetus.GrowthData);

            // ✅ Xóa thai nhi sau khi xóa dữ liệu liên quan
            _dbContext.Foetus.Remove(foetus);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateExpectedBirthDateAsync(int foetusId, DateTime expectedBirthDate)
        {
            var foetus = await _dbContext.Foetus.FirstOrDefaultAsync(f => f.FoetusId == foetusId);
            if (foetus == null || foetus.ExpectedBirthDate != null) return false;

            foetus.ExpectedBirthDate = expectedBirthDate.Date;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGestationalAgeAsync(int foetusId, int gestationalAge)
        {
            Foetus foetus = await _dbContext.Foetus.FirstOrDefaultAsync(f => f.FoetusId == foetusId);

            foetus.GestationalAge = gestationalAge;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsFoetusOwnedByUserAsync(int foetusId, int userId)
        {
            return await _dbContext.Foetus.AnyAsync(f => f.FoetusId == foetusId && f.UserId == userId);
        }

    }

}

