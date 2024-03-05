using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class ColabTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColabTable",
                columns: table => new
                {
                    ColabId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdToAddColab = table.Column<int>(nullable: false),
                    NoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColabTable", x => x.ColabId);
                    table.ForeignKey(
                        name: "FK_ColabTable_NotesTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ColabTable_UserTable_UserIdToAddColab",
                        column: x => x.UserIdToAddColab,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColabTable_NoteId",
                table: "ColabTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ColabTable_UserIdToAddColab",
                table: "ColabTable",
                column: "UserIdToAddColab");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColabTable");
        }
    }
}
