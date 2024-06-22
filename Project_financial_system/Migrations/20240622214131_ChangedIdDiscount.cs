using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_financial_system.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Discount",
                newName: "IdDiscount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDiscount",
                table: "Discount",
                newName: "Id");
        }
    }
}
