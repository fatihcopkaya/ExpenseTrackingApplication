using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery
{
    public class GetNameByAuthenticatedUserQueryResponse:IRequest<GetNameByAuthenticatedUserQueryRequest>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
