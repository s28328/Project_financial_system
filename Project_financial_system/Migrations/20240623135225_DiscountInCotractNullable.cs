using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_financial_system.Migrations
{
    /// <inheritdoc />
    public partial class DiscountInCotractNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contract",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract",
                column: "IdDiscount",
                principalTable: "Discount",
                principalColumn: "IdDiscount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contract",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract",
                column: "IdDiscount",
                principalTable: "Discount",
                principalColumn: "IdDiscount",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
