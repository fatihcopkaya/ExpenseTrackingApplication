using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafTakipYonetim.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Permission_insert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var path = Path.Combine(MigrationConfiguration.SqlsPath, "AddFirstPermission.txt");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
