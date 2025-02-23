using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class GrowthStandardServices : IGrowthStandardServices
    {
        private GrowthStandardRepository _growthStandardRepo = new();

        public async Task<List<GrowthStandard>> GetGrowthStandardsAsync()
            => await _growthStandardRepo.GetAllGrowthStandardAsync();

        public async Task AddGrowthStandardAsync(GrowthStandardDTO growthStandard)
        {
            if (_growthStandardRepo.GetGrowthStandardByAgeAsync(growthStandard.GestationalAge) != null)
            {
                throw new ArgumentException("Gestational Age already exists.");
            }

            GrowthStandard standard = new GrowthStandard
            {
                GestationalAge = growthStandard.GestationalAge,
                HcMedian = growthStandard.HcMedian,
                AcMedian = growthStandard.AcMedian,
                FlMedian = growthStandard.FlMedian,
                EfwMedian = growthStandard.EfwMedian,
            };

            await _growthStandardRepo.AddGrowthStandardAsync(standard);
        }

        public async Task UpdateGrowthStandardAsync(GrowthStandardUpdateDTO growthStandard)
        {
            var existingStandard = await _growthStandardRepo.GetGrowthStandardByAgeAsync(growthStandard.GestationalAge);

            if (existingStandard == null)
            {
                throw new ArgumentException("Growth Standard does not exist.");
            }

            // sử dụng toán tử ?? để thay thế if
            // nếu người dùng chỉ nhập 1 vài thông số thì giữ nguyên các thông số cũ
            existingStandard.HcMedian = growthStandard.HcMedian ?? existingStandard.HcMedian;
            existingStandard.AcMedian = growthStandard.AcMedian ?? existingStandard.AcMedian;
            existingStandard.FlMedian = growthStandard.FlMedian ?? existingStandard.FlMedian;
            existingStandard.EfwMedian = growthStandard.EfwMedian ?? existingStandard.EfwMedian;

            await _growthStandardRepo.UpdateGrowthStandardAsync(existingStandard);
        }
    }
}
