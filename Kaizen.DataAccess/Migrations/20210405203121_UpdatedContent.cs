using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.DataAccess.Migrations
{
    public partial class UpdatedContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_TrainingContents_TraingSubjectId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_TraingSubjectId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "TraingSubjectId",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Contents",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Contents",
                newName: "type");

            migrationBuilder.AddColumn<string>(
                name: "TraingSubjectId",
                table: "Contents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentTarget = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingContents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_TraingSubjectId",
                table: "Contents",
                column: "TraingSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_TrainingContents_TraingSubjectId",
                table: "Contents",
                column: "TraingSubjectId",
                principalTable: "TrainingContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
