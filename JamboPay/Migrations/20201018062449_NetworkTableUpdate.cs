using Microsoft.EntityFrameworkCore.Migrations;

namespace JamboPay.Migrations
{
    public partial class NetworkTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NetworkKey",
                table: "Networks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetworkKey",
                table: "Networks");
        }
    }
}
