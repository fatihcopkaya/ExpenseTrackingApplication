using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.RoleUser;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<CreateUserRoleRequestDto, UserRole>().ReverseMap();
            CreateMap<DeleteUserRoleRequestDto, UserRole>().ReverseMap();
            CreateMap<UpdateUserRoleRequestDto, UserRole>().ReverseMap();
        }
    }
}
