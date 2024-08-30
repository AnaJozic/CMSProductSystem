using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProductSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUserActiveZanimanje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zanimanje",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanimanje",
                table: "AspNetUsers");
        }
    }
}
