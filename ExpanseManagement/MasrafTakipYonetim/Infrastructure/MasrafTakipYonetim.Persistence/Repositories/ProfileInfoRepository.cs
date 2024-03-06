using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class ProfileInfoRepository : BaseRepository<ProfileInfo>, IProfileInfoRepository
    {
        public ProfileInfoRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }
        public async Task<ProfileInfo> GetProfileInfoById(Guid profileInfoId)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == profileInfoId);

        }

        public async Task<ProfileInfo> UpdateProfileInfo(ProfileInfo profileInfo)
        {
            await UpdateAsync( profileInfo);
            return profileInfo;
        }

       
        
    }
    
}
