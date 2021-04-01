using Microsoft.EntityFrameworkCore.Migrations;

namespace uploaddownloadfiles.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Contacts",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Contacts",
                newName: "Pobox");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Contacts",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "Pobox",
                table: "Contacts",
                newName: "City");
        }
    }
}
