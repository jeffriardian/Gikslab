using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Gikslab.Core.Models;

namespace Gikslab.Service.Filters
{
    public class ValidateRoleExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ValidateRoleExists(IRepositoryManager repository, ILoggerManager logger, RoleManager<IdentityRole> roleManager)
        {
            _repository = repository; _logger = logger;
            _roleManager = roleManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var role = context.ActionArguments["role"].ToString();

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                _logger.LogError($"User Profile with name: {role} doesn't exist in the database.");
                var response = new ObjectResult(new ResponseModel
                {
                    StatusCode = 404,
                    Message = $"User Profile with name: {role} doesn't exist in the database."
                });
                context.Result = response;
            }
            else
            {
                await next();
            }
        }
    }
}
