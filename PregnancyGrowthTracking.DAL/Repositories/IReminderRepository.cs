using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IReminderRepository
    {
        Task<UserReminder> CreateReminderAsync(UserReminder reminder);
        Task<IEnumerable<UserReminder>> GetReminderHistoryAsync(int userId);
        Task<bool> DeleteReminderAsync(int userId, int remindId);
        Task<bool> UpdateReminderAsync(int userId, int remindId, UpdateReminderDto request);
        Task<IEnumerable<UserReminder>> GetRemindersToSendAsync();
        Task<IEnumerable<UserReminder>> GetRemindersBeforeOneHourAsync();

    }
}
