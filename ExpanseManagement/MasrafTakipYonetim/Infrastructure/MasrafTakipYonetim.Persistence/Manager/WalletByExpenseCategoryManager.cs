using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;


namespace MasrafTakipYonetim.Persistence.Manager
{
    public class WalletByExpenseCategoryManager : IWalletByExpenseCategoryService
    {
        private readonly IWalletByExpenseCategoryRepository _repository;

        public WalletByExpenseCategoryManager(IWalletByExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

       
    }
}
