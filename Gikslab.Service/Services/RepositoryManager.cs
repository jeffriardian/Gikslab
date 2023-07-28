using AutoMapper;
using Gikslab.Core.Models;
using Gikslab.Repository.Data;
using Gikslab.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gikslab.Service.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IUserAuthenticationRepository _userAuthenticationRepository;
        private ISkillRepository _skillRepository;
        private IActivityRepository _activityRepository;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public RepositoryManager(RepositoryContext repositoryContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
            IMapper mapper, IConfiguration configuration)
        {
            _repositoryContext = repositoryContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public IUserAuthenticationRepository UserAuthentication
        {
            get
            {
                if (_userAuthenticationRepository is null)
                    _userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _roleManager, _configuration, _mapper, _repositoryContext);
                return _userAuthenticationRepository;
            }
        }
        public ISkillRepository Skill
        {
            get
            {
                if (_skillRepository is null)
                    _skillRepository = new SkillRepository(_repositoryContext);
                return _skillRepository;
            }
        }
        public IActivityRepository Activity
        {
            get
            {
                if (_activityRepository is null)
                    _activityRepository = new ActivityRepository(_repositoryContext);
                return _activityRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
