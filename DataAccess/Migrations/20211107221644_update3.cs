using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Activites",
                table: "Activites");

            migrationBuilder.RenameTable(
                name: "Activites",
                newName: "Activities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activites",
                table: "Activites",
                column: "Id");
        }
    }
}
