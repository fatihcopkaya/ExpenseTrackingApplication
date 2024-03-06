using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<CreateAppUserDto, AppUser>().ReverseMap();
            CreateMap<DeleteAppUserDto, AppUser>().ReverseMap();    
            CreateMap<UpdateAppUserDto, AppUser>().ReverseMap();
        }
    }
}
