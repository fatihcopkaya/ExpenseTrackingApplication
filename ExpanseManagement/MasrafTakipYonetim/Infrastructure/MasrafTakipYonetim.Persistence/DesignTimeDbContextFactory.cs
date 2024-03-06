using MasrafTakipYonetim.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace MasrafTakipYonetim.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MasrafTakipYonetimDbContext>
    {
        public MasrafTakipYonetimDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MasrafTakipYonetimDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new MasrafTakipYonetimDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
