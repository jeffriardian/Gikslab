using AutoMapper;
using Gikslab.Core.DTO;
using Gikslab.Core.Models;

namespace Gikslab.Core.Mappings
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<Activity, ActivityDto>();

            CreateMap<ActivityCreationDto, Activity>();

            CreateMap<ActivityUpdateDto, Activity>().ReverseMap();
        }
    }
}
