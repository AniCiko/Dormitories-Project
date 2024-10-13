using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dormitories.Migrations
{
    public partial class intial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
