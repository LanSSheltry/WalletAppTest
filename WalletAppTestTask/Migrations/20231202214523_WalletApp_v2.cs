using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletAppTestTask.Migrations
{
    /// <inheritdoc />
    public partial class WalletApp_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_status",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "due_status",
                table: "BankCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_status",
                table: "BankCards");

            migrationBuilder.AddColumn<int>(
                name: "due_status",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
