using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Service.Filters;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gikslab.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) :
            base(repository, logger, mapper)
        { }

        [HttpPost("user")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateRoleExists))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration, string role)
        {
            var userResult = await _repository.UserAuthentication.SignUpUserAsync(userRegistration, role);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : Ok(new { StatusCodeResult = StatusCode(200), userRegistration});
        }

        [HttpPost("auth/login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
        {
            return !await _repository.UserAuthentication.SignInUserAsync(user)
                ? Unauthorized()
                : Ok(new { Token = await _repository.UserAuthentication.CreateTokenAsync() });
        }
    }
}
