using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Core.Models;
using Gikslab.Repository.Data;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gikslab.Service.Services
{
    internal sealed class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context;
        private User _user;

        public UserAuthenticationRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, RepositoryContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> SignInUserAsync(UserLoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserName);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            return result;
        }

        public async Task<IdentityResult> SignUpUserAsync(UserRegistrationDto userForRegistration, string role)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);

            var skillName = userForRegistration.Skill.Select(s => s.Name).ToList();
            if (skillName != null)
            {
                foreach (var skills in skillName)
                {
                    Skill skillData = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skills);

                    if (skillData != null)
                    {
                        var skillId = skillData.Id;
                        var userId = user.Id;

                        UserSkill userSkill = new UserSkill
                        {
                            UserId = userId,
                            SkillId = skillData.Id
                        };

                        await _context.UserSkills.AddAsync(userSkill);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
