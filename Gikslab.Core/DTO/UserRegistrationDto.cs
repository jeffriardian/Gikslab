using System.ComponentModel.DataAnnotations;
using Gikslab.Core.Models;

namespace Gikslab.Core.DTO
{
    public class UserRegistrationDto
    {

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public List<GetSkillDto>? Skill { get; set; }
    }
}
