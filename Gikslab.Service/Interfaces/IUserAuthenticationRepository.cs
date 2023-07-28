using Gikslab.Core.DTO;
using Microsoft.AspNetCore.Identity;

namespace Gikslab.Service.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> SignUpUserAsync(UserRegistrationDto userForRegistration, string role);
        Task<bool> SignInUserAsync(UserLoginDto loginDto);
        Task<string> CreateTokenAsync();
    }
}
