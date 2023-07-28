using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Core.Models;
using Gikslab.Repository.Data;
using Gikslab.Service.Filters;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gikslab.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ActivityController : BaseApiController
    {
        private readonly RepositoryContext _context;
        private User _user;
        public ActivityController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, RepositoryContext context) :
            base(repository, logger, mapper)
        {
            _context = context;
        }

        [HttpPost("activity")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCreationDto activity)
        {
            try
            {
                var skillName = activity.SkillName;
                var activityData = _mapper.Map<Activity>(activity);
                await _repository.Activity.CreateActivity(skillName, activityData);
                await _repository.SaveAsync();

                var activityId = activityData.Id;
                var participant = activity.Participants.Select(p => p.Name).ToList();
                if (participant != null)
                {
                    foreach (var participants in participant)
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == participants);
                        if (user != null)
                        {
                            var userId = user.Id;

                            await _repository.Activity.AddParticipant(activityId, userId);
                        }
                    }
                }

                return StatusCode(200, "Create success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateActivity)} action {ex}");
                return StatusCode(422, "Data cannot be processed");
            }
        }

        [HttpPut("activity/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] ActivityUpdateDto activity)
        {
            try
            {
                var activityData = _mapper.Map<Activity>(activity);
                var skillName = activity.SkillName;

                await _repository.Activity.UpdateActivity(id, skillName, activityData);
                return StatusCode(200, "Update success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateActivity)} action {ex}");
                return StatusCode(422, "Data cannot be processed");
            }
        }

        [HttpDelete("activity/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var activity = _context.Activities.Include(x => x.Users).First(i => i.Id == id);
                var a = activity;
                if (activity == null) return StatusCode(422, " Data cannot be processed");
                
                _context.Remove(activity);
                await _context.SaveChangesAsync();

                return StatusCode(200, "Delete success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateActivity)} action {ex}");
                return StatusCode(422, "Data cannot be processed");
            }
        }


        [HttpGet("activities/{skill_id}")]
        public async Task<IActionResult> GetActivity(int skill_id)
        {
            try
            {
                var activity = await _repository.Activity.GetActivity(skill_id);
                return Ok(activity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetActivity)} action {ex}");
                return StatusCode(422, "Data cannot be processed");
            }
        }
    }
}