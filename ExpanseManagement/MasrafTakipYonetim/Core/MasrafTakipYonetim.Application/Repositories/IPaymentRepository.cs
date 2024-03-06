using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IDataResult<List<Payment>>> GetPaymentListAsync();
        Task<IDataResult<List<Payment>>> GetPaymentsByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType);
        Task<Payment> GetPaymentByIdAsync(Guid paymentId);

       // Task<IDataResult<Payment>> CreatePaymentAsync(Payment payment);
        void CreatePayment(Payment payment);

        Task<Payment> UpdatePaymentAsync(Payment payment); // Güncelleme yöntemi

        Task<Payment> DeletePaymentAsync(Payment payment);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<double?> GetTotalAmountByExpenseCategoryType(ExpenseCategoryType expenseCategoryType);
        Task<bool> GetPaymentListByDate(int mounth,int year,Guid userId);
    }
}
