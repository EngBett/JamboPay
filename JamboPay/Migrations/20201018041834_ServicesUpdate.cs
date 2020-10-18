using Microsoft.EntityFrameworkCore.Migrations;

namespace JamboPay.Migrations
{
    public partial class ServicesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CommissionPercentage",
                table: "Services",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionPercentage",
                table: "Services");
        }
    }
}
