using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class ListAppUserPaymentResponse:IRequest<ListAppUserPaymentRequest>
    {
        public List<AppUser>? AppUsers { get; set; }
        public string Messages { get; set; }
    }
}
