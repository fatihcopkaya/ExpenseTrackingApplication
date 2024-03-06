
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery
{
    public class GetWalletResponse : IRequest<GetWalletRequest>
    {
        public Wallet Wallet { get; set; }
    }
}
