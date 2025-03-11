using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IReminderService
    {
        Task<ReminderResponseDto> CreateReminderAsync(int userId, CreateReminderDto request);
        Task<IEnumerable<ReminderHistoryDto>> GetReminderHistoryAsync(int userId);
        Task<bool> DeleteReminderAsync(int userId, int remindId);
        Task<bool> UpdateReminderAsync(int userId, int remindId, UpdateReminderDto request);
        //Task SendRemindersBeforeOneHourAsync();

    }
}
