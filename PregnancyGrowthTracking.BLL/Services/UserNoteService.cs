using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class UserNoteService : IUserNoteService
    {
        private readonly IUserNoteRepository _noteRepository;

        public UserNoteService(IUserNoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<List<UserNote>> GetAllNotesAsync()
        {
            return await _noteRepository.GetAllAsync();
        }

        public async Task<UserNote?> GetNoteByIdAsync(int noteId)
        {
            return await _noteRepository.GetByIdAsync(noteId);
        }

        public async Task AddNoteAsync(UserNote note)
        {
            await _noteRepository.AddAsync(note);
        }

        public async Task UpdateNoteAsync(UserNote note)
        {
            await _noteRepository.UpdateAsync(note);
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            await _noteRepository.DeleteAsync(noteId);
        }
    }
}