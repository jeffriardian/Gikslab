using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gikslab.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class SkillController : BaseApiController
    {
        public SkillController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) : base(repository, logger, mapper) { }

        [HttpGet("skills")]
        [ResponseCache(CacheProfileName = "30SecondsCaching")]
        public async Task<IActionResult> GetSkills()
        {
            try
            {
                var skills = await _repository.Skill.GetAllSkills(trackChanges: false);
                var skillsDto = _mapper.Map<IEnumerable<SkillDto>>(skills);
                return Ok(skillsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSkills)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
