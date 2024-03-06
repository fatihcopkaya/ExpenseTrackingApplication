using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.Wallet;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.WalletQueries
{
    public class GetPaymentsAndExpenseFromWalletHandler : IRequestHandler<GetPaymentsAndExpenseFromWalletRequest, GetPaymentsAndExpenseFromWalletResponse>
    {
        private readonly IWalletRepository _walletRepository;

        public GetPaymentsAndExpenseFromWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<GetPaymentsAndExpenseFromWalletResponse> Handle(GetPaymentsAndExpenseFromWalletRequest request, CancellationToken cancellationToken)
        {
            var (totalPayments, totalExpenses) = await _walletRepository.GetTotalPaymentsAndExpensesAsync();

            if (totalPayments == 0 && totalExpenses == 0)
            {
                return new GetPaymentsAndExpenseFromWalletResponse
                {
                    TotalPayments = new float[0],
                    TotalExpenses = new float[0]
                };
            }

            var walletInfoDto = new GetWalletInfoDto
            {
                TotalPayments = totalPayments,
                TotalExpenses = totalExpenses
            };

            return new GetPaymentsAndExpenseFromWalletResponse
            {
                TotalPayments = new float[] { walletInfoDto.TotalPayments },
                TotalExpenses = new float[] { walletInfoDto.TotalExpenses }
            };
            //return new List<GetWalletInfoDto>();




        }
    }
    
}
