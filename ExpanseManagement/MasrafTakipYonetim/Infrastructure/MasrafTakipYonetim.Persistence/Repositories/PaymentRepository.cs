using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {

        private readonly MasrafTakipYonetimDbContext _context;
        public PaymentRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
            _context = context;
        }



        //public async Task<IDataResult<Payment>> CreatePaymentAsync(Payment payment)
        //{
        //   await AddAsync(payment);
        //    return new SuccessDataResult<Payment>(Messages.addMessage);
        //}

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await AddAsync(payment);
            return payment;
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid paymentId)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == paymentId);

        }

        public async Task<IDataResult<List<Payment>>> GetPaymentListAsync()
        {
            var list= await GetListAsync(x=>x.IsDeleted== false, includes:new Expression<Func<Payment, object>>[] {x=>x.AppUser,x=>x.ExpenseCategory});

            return new SuccessDataResult<List<Payment>>(list.ToList());

           
        }
       

       

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
          await UpdateAsync(payment);
            return payment;
        }

        public async Task<Payment> DeletePaymentAsync(Payment payment)
        {
            await UpdateAsync(payment);
            return payment;
        }

        //async Task<IDataResult<List<Payment>>> GetPaymentsExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType)
        //{

        //    var list = await GetListAsync(x => x.IsDeleted == false && x.ExpenseCategory.ExpenseCategoryType== expenseCategoryType, includes: new Expression<Func<Payment, object>>[] { x => x.AppUser, x => x.ExpenseCategory });
        //    return new SuccessDataResult<List<Payment>>(list.ToList());
        //}

        public async Task<IDataResult<List<Payment>>> GetPaymentsByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType)
        {
            var list = await GetListAsync(x => x.IsDeleted == false && x.ExpenseCategory.ExpenseCategoryType == expenseCategoryType, includes: new Expression<Func<Payment, object>>[] { x => x.AppUser, x => x.ExpenseCategory });
            return new SuccessDataResult<List<Payment>>(list.ToList());//ıDATA result kalkıcak
        }

        public async Task<double?> GetTotalAmountByExpenseCategoryType(ExpenseCategoryType expenseCategoryType)
        {
            double? totalAmount = await _context.Payments
                .Where(p=>p.ExpenseCategory.ExpenseCategoryType==expenseCategoryType && p.IsDeleted == false)
                .SumAsync(p=>(double?)p.PaymentAmount);
            return totalAmount;
        }

        public void CreatePayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetPaymentListByDate(int mounth, int year, Guid userId)
        {
           var paymentCheckForUser = await GetFirstOrDefaultAsync(x=>x.PeriodDate.Month == mounth&&x.PeriodDate.Year == year && x.AppUserId == userId);
            return paymentCheckForUser != null ? true : false; 
        }
    }
}

