using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StandardCMS.DB.Migrations
{
    /// <inheritdoc />
    public partial class addedSaleAmountCoumnInComsnRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SaleAmount",
                table: "CommissionRules",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleAmount",
                table: "CommissionRules");
        }
    }
}
