using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_financial_system.Migrations
{
    /// <inheritdoc />
    public partial class contractupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTerminated",
                table: "Contract",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTerminated",
                table: "Contract");
        }
    }
}
