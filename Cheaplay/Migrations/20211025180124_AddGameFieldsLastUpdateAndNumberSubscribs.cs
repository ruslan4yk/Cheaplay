using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cheaplay.Migrations
{
    public partial class AddGameFieldsLastUpdateAndNumberSubscribs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Games",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<int>(
                name: "NumberSubscribes",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("dbo.Games", "LastUpdate");
            migrationBuilder.DropColumn("dbo.Games", "NumberSubscribes");
        }
    }
}
