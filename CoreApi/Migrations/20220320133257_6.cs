using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "TribeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "TribeHistory");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Points30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Points7",
                table: "TribeCurrent");
        }
    }
}
