using Gikslab.Core.Models;
using Gikslab.Repository.Data;
using Gikslab.Repository.GenericRepository.Service;
using Gikslab.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gikslab.Service.Services
{
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public SkillRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Skill>> GetAllSkills(bool trackChanges)
            => await FindAllAsync(trackChanges).Result.OrderBy(c => c.Name).ToListAsync();

        public async Task<Skill?> GetSkill(int skillId, bool trackChanges)
            => await FindByConditionAsync(c => c.Id.Equals(skillId), trackChanges).Result.SingleOrDefaultAsync();
    }
}
