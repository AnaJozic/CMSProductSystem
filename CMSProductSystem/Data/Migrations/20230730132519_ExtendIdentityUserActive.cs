using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProductSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUserActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Aktivan",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivan",
                table: "AspNetUsers");
        }
    }
}
