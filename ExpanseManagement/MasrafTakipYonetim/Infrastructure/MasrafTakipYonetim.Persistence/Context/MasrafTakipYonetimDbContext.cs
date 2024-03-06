using MasrafTakipYonetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace MasrafTakipYonetim.Persistence.Context
{
    public class MasrafTakipYonetimDbContext : DbContext
    {
        public MasrafTakipYonetimDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser>AppUsers  { get; set; }
        public DbSet<ExpenseCategory>ExpenseCategories { get; set; }

        public DbSet<Expense>Expenses { get; set; }

        public DbSet<ExpenseType>ExpensesType { get; set;}

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Roles>Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ProfileInfo> ProfileInfos { get; set; }
        public DbSet<WalletByExpenseCategory> WalletByExpensesCategory { get; set; }

        public DbSet<Wallet> Wallets { get;}

        public DbSet<Roles> Role { get; set; }
        public DbSet<UserExpenseCategory> UserExpenseCategory { get; set; }


    }
}
