using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheaplay.Migrations
{
    public partial class AddDiscountUpdatesConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Updates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Updates_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Updates_GameId",
                table: "Updates",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Updates");
        }
    }
}
