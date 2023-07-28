using Gikslab.Core.DTO;
using Gikslab.Core.Models;

namespace Gikslab.Service.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetAllSkills(bool trackChanges);
        Task<Skill> GetSkill(int skillId, bool trackChanges);
    }
}
