using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheaplay.Migrations
{
    public partial class AddGameIdSharkApiField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSharkAPI",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSharkAPI",
                table: "Games");
        }
    }
}
