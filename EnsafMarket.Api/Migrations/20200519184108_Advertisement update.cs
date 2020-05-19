using Microsoft.EntityFrameworkCore.Migrations;

namespace EnsafMarket.Api.Migrations
{
    public partial class Advertisementupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "Adertisement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Adertisement",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Adertisement");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Adertisement");
        }
    }
}
