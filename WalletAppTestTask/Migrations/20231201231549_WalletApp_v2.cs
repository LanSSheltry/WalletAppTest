using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WalletAppTestTask.Migrations
{
    /// <inheritdoc />
    public partial class WalletApp_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_uid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "authorized_uid",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Transactions",
                newName: "id_bank_card");

            migrationBuilder.RenameColumn(
                name: "icon_path",
                table: "Transactions",
                newName: "icon");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_uid",
                table: "Transactions",
                newName: "IX_Transactions_id_bank_card");

            migrationBuilder.AddColumn<string>(
                name: "authorized_user_name",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BankCard",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uid = table.Column<long>(type: "bigint", nullable: false),
                    id_bank = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCard", x => x.id);
                    table.ForeignKey(
                        name: "FK_BankCard_Bank_id_bank",
                        column: x => x.id_bank,
                        principalTable: "Bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankCard_Users_uid",
                        column: x => x.uid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCard_id_bank",
                table: "BankCard",
                column: "id_bank");

            migrationBuilder.CreateIndex(
                name: "IX_BankCard_uid",
                table: "BankCard",
                column: "uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankCard_id_bank_card",
                table: "Transactions",
                column: "id_bank_card",
                principalTable: "BankCard",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankCard_id_bank_card",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BankCard");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropColumn(
                name: "authorized_user_name",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "id_bank_card",
                table: "Transactions",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "icon",
                table: "Transactions",
                newName: "icon_path");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_id_bank_card",
                table: "Transactions",
                newName: "IX_Transactions_uid");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "authorized_uid",
                table: "Transactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_uid",
                table: "Transactions",
                column: "uid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
