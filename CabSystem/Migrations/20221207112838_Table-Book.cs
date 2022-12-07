using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class TableBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Locations");
        }
    }
}
