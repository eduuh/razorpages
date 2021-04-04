using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.DataAccess.Migrations
{
    public partial class UpdatedClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capacity",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Classes");
        }
    }
}
