using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheaplay.Migrations
{
    public partial class AddGameFieldsLastUpdateAndNumberSubscribs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberSubscribes",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NumberSubscribes",
                table: "Games");
        }
    }
}
