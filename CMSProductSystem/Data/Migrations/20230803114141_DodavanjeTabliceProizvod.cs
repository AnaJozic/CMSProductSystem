using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProductSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodavanjeTabliceProizvod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Proizvod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proizvod",
                table: "Proizvod",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Proizvod",
                table: "Proizvod");

            migrationBuilder.RenameTable(
                name: "Proizvod",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");
        }
    }
}
