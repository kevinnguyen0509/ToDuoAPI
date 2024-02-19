using AutoMapper;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Configurations
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<Adventures, AdventureDto>().ReverseMap();
            CreateMap<ToDuoUsers, ApiUserDTO>().ReverseMap();
            CreateMap<ToDuoUsers, ApiRegisterUserDTO>().ReverseMap();
            CreateMap<ToDuoUsers, ToDuoBasicUsersDTO>().ReverseMap();
            CreateMap<AdventureBasicUserMatchDTO, ToDuoBasicUsersDTO>().ReverseMap();
        }
    }
}
