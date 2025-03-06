using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IUserNoteRepository
    {
        Task<List<UserNote>> GetAllAsync();
        Task<UserNote?> GetByIdAsync(int noteId);
        Task AddAsync(UserNote note);
        Task UpdateAsync(UserNote note);
        Task SaveChangesAsync();
        Task DeleteAsync(int noteId);
        Task<IEnumerable<UserNote>> GetNotesByUserIdAsync(int userId);
    }
}
