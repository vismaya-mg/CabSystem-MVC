using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabSystem.Migrations
{
    public partial class AddCabTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vehicle = table.Column<int>(type: "int", nullable: false),
                    VechicleNumber = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_UserId",
                table: "Cabs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cabs");
        }
    }
}
