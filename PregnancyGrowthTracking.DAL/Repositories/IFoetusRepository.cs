using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IFoetusRepository
    {
        Task<Foetus> CreateFoetusAsync(Foetus foetus);
        Task<bool> IsFoetusNameExistsAsync(int userId, string name);
        Task<IEnumerable<Foetus>> GetFoetusesByUserIdAsync(int userId);
        Task<Foetus?> GetFoetusByIdAsync(int foetusId);
        Task<bool> DeleteFoetusAsync(int foetusId, int userId);
        Task<bool> UpdateExpectedBirthDateAsync(int foetusId, DateTime expectedBirthDate);
        Task<bool> UpdateGestationalAgeAsync(int foetusId, int newGestationalAge);
        Task<bool> IsFoetusOwnedByUserAsync(int foetusId, int userId);

    }
}
