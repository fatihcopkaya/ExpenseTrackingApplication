using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



#nullable disable

namespace MasrafTakipYonetim.Persistence.Migrations
{
    
    public partial class Wallet_insert : Migration
    {
       

        
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var path = Path.Combine(MigrationConfiguration.SqlsPath, "AddFirstWallet.txt");

            // SQL metnini okuma ve çalıştırma
            if (File.Exists(path))
            {
                string sqlText = File.ReadAllText(path);
                migrationBuilder.Sql(sqlText);
            }
            else
            {
                throw new ArgumentNullException(nameof(path));
            }
        }


    

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
