using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class CreateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypes",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTypes",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
