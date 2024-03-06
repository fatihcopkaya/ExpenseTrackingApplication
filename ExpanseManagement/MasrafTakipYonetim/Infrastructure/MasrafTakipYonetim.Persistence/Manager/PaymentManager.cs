using Hangfire;
using MasrafTakipYonetim.Application.Helpers;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;

namespace MasrafTakipYonetim.Persistence.Manager
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAppUserRepository _appUserRepositoy;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public PaymentManager(IPaymentRepository paymentRepository, IAppUserRepository appUserRepositoy, IUserExpenseCategoryRepository userExpenseCategoryRepository, IMailHelper mailHelper, IRolePermissionRepository rolePermissionRepository)
        {
            _paymentRepository = paymentRepository;
            _appUserRepositoy = appUserRepositoy;
            _userExpenseCategoryRepository = userExpenseCategoryRepository;
            _mailHelper = mailHelper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        [JobDisplayName("Create Payment For Users")]
        [Queue("default")]
        public async Task CreatePaymentsJob()
        {
            //job çalıştıkça kullanıcıların ödemesi var mı diye kontrol edip yoksa oluşturacak

            var userList = _appUserRepositoy.GetAllAppUsers();
            var dateTime = DateTime.Now;

            foreach (var user in userList)
            {
                var paymentsbyPeriod = await _paymentRepository.GetPaymentListByDate(dateTime.Month, dateTime.Year, user.Id);
                var userExpenseCategory =  _userExpenseCategoryRepository.GetListUserExpenseCategoryById(user.Id);
                var permission = await _rolePermissionRepository.GetRolePermissionByRoleId(user.RoleId);
                var list = permission.Data.Where(x => x.Permission.PermissionName == "Kahve Kullanıcısı");




                if (!paymentsbyPeriod && userExpenseCategory.Any())
                {


                    var newPayments = userExpenseCategory.Select(uec => new Payment
                    {
                        AppUserId = user.Id,
                        PaymentAmount = 0,
                        PeriodDate = DateTime.Now,
                        ExpenseCategoryId = uec.ExpenseCategoryId,
                        IsPaid = false,

                    }).ToList();
                    foreach (var item in newPayments)
                    {
                        await _paymentRepository.CreatePaymentAsync(item);

                    }
                    await _mailHelper.SendMail(user.Email, "Ödeme Bilgisi", $"{user.FirstName} Kullanıcısının ödemesi oluşturuldu.");


                   
                    

                     

                }

            }
        }

        [JobDisplayName("Create Payment of Mounth")]
        [Queue("default")]
        public async Task CreatePaymentsofMountJob()
        {
            //job çalıştıkça her bir kullanıcı için ödemesi false olan bir payment oluşturulacak
            var userList = _appUserRepositoy.GetAllAppUsers();
            foreach (var user in userList)
            {
                var UserExpenseCategory = _userExpenseCategoryRepository.GetListUserExpenseCategoryById(user.Id);
                var newPayments = UserExpenseCategory.Select(uec => new Payment
                {
                    AppUserId = user.Id,
                    PaymentAmount = 0,
                    PeriodDate = DateTime.Now,
                    ExpenseCategoryId = uec.ExpenseCategoryId,
                    IsPaid = false,

                }).ToList();
                foreach (var item in newPayments)
                {
                    await _paymentRepository.CreatePaymentAsync(item);
                }


            }


        }
    }
    }

    
    

