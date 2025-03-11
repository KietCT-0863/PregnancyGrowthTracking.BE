using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IUserNoteService
    {
        Task<List<UserNote>> GetAllNotesAsync();
        Task<UserNote?> GetNoteByIdAsync(int noteId);
        Task AddNoteAsync(UserNote note);
        Task UpdateNoteAsync(UserNote note);
        Task SaveChangesAsync();
        Task DeleteNoteAsync(int noteId);
        Task<IEnumerable<UserNote>> GetNotesByUserIdAsync(int userId);
    }
}