using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Core.Models;

namespace Gikslab.Core.Mappings
{
    public class SkillMappingProfile : Profile
    {
        public SkillMappingProfile()
        {
            CreateMap<Skill, SkillDto>();
        }
    }
}
