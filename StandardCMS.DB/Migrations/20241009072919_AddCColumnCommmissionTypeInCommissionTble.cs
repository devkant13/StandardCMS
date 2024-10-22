using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StandardCMS.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddCColumnCommmissionTypeInCommissionTble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommissionType",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionType",
                table: "Commissions");
        }
    }
}
