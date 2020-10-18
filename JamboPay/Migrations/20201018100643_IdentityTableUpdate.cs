using Microsoft.EntityFrameworkCore.Migrations;

namespace JamboPay.Migrations
{
    public partial class IdentityTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserNetworks_ApplicationUserId",
                table: "UserNetworks");

            migrationBuilder.CreateIndex(
                name: "IX_UserNetworks_ApplicationUserId",
                table: "UserNetworks",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserNetworks_ApplicationUserId",
                table: "UserNetworks");

            migrationBuilder.CreateIndex(
                name: "IX_UserNetworks_ApplicationUserId",
                table: "UserNetworks",
                column: "ApplicationUserId",
                unique: true);
        }
    }
}
