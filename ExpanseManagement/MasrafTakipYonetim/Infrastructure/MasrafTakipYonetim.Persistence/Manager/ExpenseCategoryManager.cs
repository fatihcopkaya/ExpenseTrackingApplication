using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;

namespace MasrafTakipYonetim.Persistence.Manager
{
    public class ExpenseCategoryManager : IExpenseCategoryService
    {

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public ExpenseCategoryManager(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

       
    }
}
