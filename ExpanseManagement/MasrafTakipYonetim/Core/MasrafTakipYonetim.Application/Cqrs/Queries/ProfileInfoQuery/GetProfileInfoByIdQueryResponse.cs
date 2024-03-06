using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery
{
    public class GetProfileInfoByIdQueryResponse : IRequest<GetProfileInfoByIdQueryRequest>
    {
        public AppUser? AppUser { get; set; }
        public string Message { get; set; }
    }
}
