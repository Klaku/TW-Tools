using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Villages",
                table: "TribeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Villages",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Villages30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Villages7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Villages",
                table: "TribeHistory");

            migrationBuilder.DropColumn(
                name: "Villages",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Villages30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Villages7",
                table: "TribeCurrent");
        }
    }
}
