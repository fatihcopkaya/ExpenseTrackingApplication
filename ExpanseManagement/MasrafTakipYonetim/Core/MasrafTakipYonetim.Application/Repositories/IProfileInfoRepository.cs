using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IProfileInfoRepository:IBaseRepository<ProfileInfo>
    {
        Task<ProfileInfo> UpdateProfileInfo(ProfileInfo profileInfo);
        Task<ProfileInfo> GetProfileInfoById(Guid profileInfoId);
    }
}
