using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MasrafTakipYonetim.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MasrafTakipYonetim.Application.Mapping
{
    public class RoleProfile:Profile
    {


        public RoleProfile()
        {
            CreateMap<CreateRoleDto,Roles>().ReverseMap();

            CreateMap<UpdateRoleDto,Roles>().ReverseMap();

            CreateMap<DeleteRoleDto,Roles>().ReverseMap();

            
        }


    }
}
