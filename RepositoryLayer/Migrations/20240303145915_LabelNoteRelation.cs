using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelNoteRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "NotesTable",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LabelID",
                table: "LabelTable",
                newName: "LabelId");

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "NotesTable",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LabelName",
                table: "LabelTable",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_LabelId",
                table: "NotesTable",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_LabelTable_LabelId",
                table: "NotesTable",
                column: "LabelId",
                principalTable: "LabelTable",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_LabelTable_LabelId",
                table: "NotesTable");

            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable");

            migrationBuilder.DropIndex(
                name: "IX_NotesTable_LabelId",
                table: "NotesTable");

            migrationBuilder.DropIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesTable");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "NotesTable");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NotesTable",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "LabelId",
                table: "LabelTable",
                newName: "LabelID");

            migrationBuilder.AlterColumn<int>(
                name: "LabelName",
                table: "LabelTable",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
