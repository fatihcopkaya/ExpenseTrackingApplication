using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class GetAppUserByEmailQueryRequest : IRequest<GetAppUserByEmailQueryResponse>
    {
        public string Email { get; set; }
    }
}
