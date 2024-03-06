using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class GetAppUserCountQueryResponse
    {
        public int AppUserCount { get; set; } 
        //public AppUserListQueryResponse appUserListQueryResponse { get; set; } //appuserlist te gerekli mi count almak için

    }
}
