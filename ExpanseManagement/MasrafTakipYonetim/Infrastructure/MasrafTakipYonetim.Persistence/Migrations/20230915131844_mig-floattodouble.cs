﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafTakipYonetim.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migfloattodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<float>(
            //    name: "Amount",
            //    table: "Expenses",
            //    type: "real",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<double>(
            //    name: "Amount",
            //    table: "Expenses",
            //    type: "float",
            //    nullable: false,
            //    oldClrType: typeof(float),
            //    oldType: "real");
        }
    }
}
