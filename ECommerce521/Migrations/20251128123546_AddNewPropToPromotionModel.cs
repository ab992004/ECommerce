using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce521.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropToPromotionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Promotions");
        }
    }
}
