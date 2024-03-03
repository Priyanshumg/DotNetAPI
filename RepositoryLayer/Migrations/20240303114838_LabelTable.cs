using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Reminder",
                table: "NotesTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "NotesTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelTable");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "NotesTable");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "NotesTable");
        }
    }
}
