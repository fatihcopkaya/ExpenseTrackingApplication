using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;


namespace MasrafTakipYonetim.Persistence.Manager
{
    public class WalletManager : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletManager(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

       
    }
}
