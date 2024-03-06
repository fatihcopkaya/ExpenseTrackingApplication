using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class AppUserListQueryResponse : IRequest<AppUserListQueryRequest>
    {
        public List<AppUser>? AppUsers { get; set; }
        public string Messages { get; set; }
    }
}
