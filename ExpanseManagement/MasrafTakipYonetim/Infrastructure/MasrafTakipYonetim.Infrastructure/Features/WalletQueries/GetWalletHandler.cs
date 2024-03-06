using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.WalletQueries
{
    public class GetWalletHandler : IRequestHandler<GetWalletRequest, GetWalletResponse>
    {
        private readonly IWalletRepository _walletRepository;

        public GetWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<GetWalletResponse> Handle(GetWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetFirstOrDefaultAsync();
            return wallet == null ? throw new MasrafTakipCustomException($"{nameof(Wallet)} Could Not Be Found", 404)
            : new GetWalletResponse { Wallet = wallet };
        }
    }
}
