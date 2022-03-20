using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points30",
                table: "VillageCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points7",
                table: "VillageCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking30",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking7",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VillagesCount30",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VillagesCount7",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points30",
                table: "VillageCurrent");

            migrationBuilder.DropColumn(
                name: "Points7",
                table: "VillageCurrent");

            migrationBuilder.DropColumn(
                name: "RA30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RA7",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RO30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RO7",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RS30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RS7",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Ranking30",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Ranking7",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Points30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "Points7",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RA30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RA7",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RO30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RO7",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RS30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RS7",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "Ranking30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "Ranking7",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "VillagesCount30",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "VillagesCount7",
                table: "PlayerCurrents");
        }
    }
}
