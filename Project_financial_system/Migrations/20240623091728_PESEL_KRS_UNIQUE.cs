using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_financial_system.Migrations
{
    /// <inheritdoc />
    public partial class PESEL_KRS_UNIQUE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Individual_Customer_PESEL",
                table: "Individual_Customer",
                column: "PESEL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Customer_KRS",
                table: "Company_Customer",
                column: "KRS",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Individual_Customer_PESEL",
                table: "Individual_Customer");

            migrationBuilder.DropIndex(
                name: "IX_Company_Customer_KRS",
                table: "Company_Customer");
        }
    }
}
