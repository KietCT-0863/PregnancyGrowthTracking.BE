using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class UserNoteRepository : IUserNoteRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _context;

        public UserNoteRepository(PregnancyGrowthTrackingDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserNote>> GetAllAsync()
        {
            return await _context.UserNotes.ToListAsync();
        }

        public async Task<UserNote?> GetByIdAsync(int noteId)
        {
            return await _context.UserNotes.FindAsync(noteId);
        }

        public async Task AddAsync(UserNote note)
        {
            _context.UserNotes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserNote note)
        {
            _context.UserNotes.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int noteId)
        {
            var note = await _context.UserNotes.FindAsync(noteId);
            if (note != null)
            {
                _context.UserNotes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}

