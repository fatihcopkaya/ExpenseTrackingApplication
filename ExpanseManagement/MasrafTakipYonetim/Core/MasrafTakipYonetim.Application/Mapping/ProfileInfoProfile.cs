using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.ProfileInfo;
using MasrafTakipYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Mapping
{
    public class ProfileInfoProfile : Profile
    {
        public ProfileInfoProfile() {
            CreateMap<UpdateProfileInfoDto, ProfileInfo>().ReverseMap();

        }
    }
}
