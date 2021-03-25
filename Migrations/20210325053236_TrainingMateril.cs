using Microsoft.EntityFrameworkCore.Migrations;

namespace uploaddownloadfiles.Migrations
{
    public partial class TrainingMateril : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Schools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraingSubjectId",
                table: "Contents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentTarget = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingContents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ContactId",
                table: "Schools",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_TraingSubjectId",
                table: "Contents",
                column: "TraingSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContactId",
                table: "AspNetUsers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contacts_ContactId",
                table: "AspNetUsers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_TrainingContents_TraingSubjectId",
                table: "Contents",
                column: "TraingSubjectId",
                principalTable: "TrainingContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Contacts_ContactId",
                table: "Schools",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contacts_ContactId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_TrainingContents_TraingSubjectId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Contacts_ContactId",
                table: "Schools");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ContactId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Contents_TraingSubjectId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContactId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "TraingSubjectId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "AspNetUsers");
        }
    }
}
