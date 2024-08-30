using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProductSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodavanjeTabliceKategorija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_CategoryID",
                table: "Proizvod",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvod_Kategorija_CategoryID",
                table: "Proizvod",
                column: "CategoryID",
                principalTable: "Kategorija",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvod_Kategorija_CategoryID",
                table: "Proizvod");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropIndex(
                name: "IX_Proizvod_CategoryID",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Proizvod");
        }
    }
}
