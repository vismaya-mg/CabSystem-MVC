using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class CreateCabSingleForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabs_UserId",
                table: "Cabs");

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_UserId",
                table: "Cabs",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabs_UserId",
                table: "Cabs");

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_UserId",
                table: "Cabs",
                column: "UserId");
        }
    }
}
