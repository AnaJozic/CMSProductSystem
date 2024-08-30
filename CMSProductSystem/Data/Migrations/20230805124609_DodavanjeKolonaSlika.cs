using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProductSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodavanjeKolonaSlika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Proizvod");
        }
    }
}
