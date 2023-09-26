using AutoMapper;
using MovieAPIs.Models.Domain;
using PortFolio.Models.DTO;

namespace PortFolio.Helper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDataDTO>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, UserDataDTO>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        }

    }
}
