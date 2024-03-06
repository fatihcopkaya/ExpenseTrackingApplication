using MasrafTakipYonetim.Application.Mapping;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Manager;
using MasrafTakipYonetim.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MasrafTakipYonetim.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<MasrafTakipYonetimDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoriesRepository>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryManager>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseService, ExpenseManager>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<IExpenseTypeService, ExpenseTypeManager>();
            services.AddScoped<IWalletByExpenseCategoryRepository, WalletByExpenseCategoryRepository>();
            services.AddScoped<IWalletByExpenseCategoryService, WalletByExpenseCategoryManager>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletManager>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionManager>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRolePermissionService, RolePermissionManager>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentManager>();
            services.AddScoped<IUserExpenseCategoryRepository, UserExpenseCategoryRepository>();
            services.AddScoped<IProfileInfoRepository, ProfileInfoRepository>();
            

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AppUserProfile));


        }
    }
}
