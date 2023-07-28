using Gikslab.Core.Models;
using Gikslab.Repository.Data;
using Gikslab.Repository.GenericRepository.Service;
using Gikslab.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gikslab.Service.Services
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(RepositoryContext repositoryContext) : base(repositoryContext){ }

        public async Task AddParticipant(int activityId, string userId)
        {
            UserActivity userActivity = new UserActivity
            {
                UserId = userId,
                ActivityId = activityId,
            };

            await RepositoryContext.UserActivities.AddAsync(userActivity);
            await RepositoryContext.SaveChangesAsync();
        }

        public async Task CreateActivity(string skillName, Activity activity)
        {
            var skills = await RepositoryContext.Skills.FirstOrDefaultAsync(s => s.Name == skillName);
            if (skills != null)
            {
                activity.SkillId = skills.Id;
                await CreateAsync(activity);
            }
        }

        public async Task UpdateActivity(int id, string skillName, Activity activity)
        {
            var skills = await RepositoryContext.Skills.FirstOrDefaultAsync(s => s.Name == skillName);
            if (skills != null)
            {
                activity.SkillId = skills.Id;
                var newActivity = RepositoryContext.Activities.Find(id);
                var newA = newActivity;
                if (newActivity != null)
                {
                    newActivity.SkillId = activity.SkillId;
                    newActivity.Title = activity.Title;
                    newActivity.Description = activity.Description;
                    newActivity.Startdate = activity.Startdate;
                    newActivity.Enddate = activity.Enddate;

                    await RepositoryContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<object>> GetActivity(int skillId)
        {
            var result = (from a in RepositoryContext.Activities.Include(a => a.Users)
                          join s in RepositoryContext.Skills on a.SkillId equals s.Id
                          where s.Id == skillId
                          select new
                          {
                              skill_id = skillId,
                              skill_name = s.Name,
                              title = a.Title,
                              description = a.Description,
                              startdate = a.Startdate,
                              enddate = a.Enddate,
                              participant = a.Users.Select(u => new
                              {
                                  id = u.Id,
                                  name = u.Name,
                                  //profile = u.Id,
                                  skill = u.Skills.Select(s => s.Name)
                              })
                          }).ToListAsync();

            return await result;
        }
    }
}
