using Gikslab.Core.Models;

namespace Gikslab.Service.Interfaces
{
    public interface IActivityRepository
    {
        Task<IEnumerable<object>> GetActivity(int skillId);
        Task CreateActivity(string skillName, Activity activity);
        Task UpdateActivity(int id, string skillName, Activity activity);
        Task AddParticipant(int activityId, string userId);
    }
}
