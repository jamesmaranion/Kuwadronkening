using Microsoft.EntityFrameworkCore.Migrations;

namespace Kuwadro.Migrations
{
    public partial class AddedHomeArt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "artList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Popular",
                table: "artList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Recommended",
                table: "artList",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Featured",
                table: "artList");

            migrationBuilder.DropColumn(
                name: "Popular",
                table: "artList");

            migrationBuilder.DropColumn(
                name: "Recommended",
                table: "artList");
        }
    }
}
