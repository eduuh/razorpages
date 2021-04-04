using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.DataAccess.Migrations
{
    public partial class UpdatedSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Schools");
        }
    }
}
