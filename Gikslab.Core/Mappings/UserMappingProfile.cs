using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Core.Models;

namespace Gikslab.Core.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
