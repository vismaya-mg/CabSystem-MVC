using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class UpdateIntString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_DriverId",
                table: "Books",
                column: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
