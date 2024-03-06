
using MediatR;




namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class GetAppUserByIdQueryRequest : IRequest<GetAppUserByIdQueryResponse>

    {
        public Guid Id { get; set; }
       

 

    }
}
