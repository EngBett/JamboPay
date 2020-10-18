using Microsoft.EntityFrameworkCore.Migrations;

namespace JamboPay.Migrations
{
    public partial class TransactionColsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Networks_NetworkId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_NetworkId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Commissions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_TransactionId",
                table: "Commissions",
                column: "TransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Transactions_TransactionId",
                table: "Commissions",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Transactions_TransactionId",
                table: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_TransactionId",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Commissions");

            migrationBuilder.AddColumn<string>(
                name: "NetworkId",
                table: "Transactions",
                type: "varchar(255) CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_NetworkId",
                table: "Transactions",
                column: "NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Networks_NetworkId",
                table: "Transactions",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
