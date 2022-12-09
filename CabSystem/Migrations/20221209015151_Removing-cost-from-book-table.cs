using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class Removingcostfrombooktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
