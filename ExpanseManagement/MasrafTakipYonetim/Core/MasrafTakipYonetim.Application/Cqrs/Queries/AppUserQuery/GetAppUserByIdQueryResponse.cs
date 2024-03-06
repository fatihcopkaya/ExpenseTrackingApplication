using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{

    public class GetAppUserByIdQueryResponse : IRequest<GetAppUserByIdQueryRequest>
    {
        public AppUser? AppUser { get; set; }
        public string Message { get; set; }


    }
}
