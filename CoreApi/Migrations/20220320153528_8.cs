using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points24",
                table: "VillageCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Villages24",
                table: "TribeCurrent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RA24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RO24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RS24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ranking24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VillagesCount24",
                table: "PlayerCurrents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points24",
                table: "VillageCurrent");

            migrationBuilder.DropColumn(
                name: "Points24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RA24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RO24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "RS24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Ranking24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Villages24",
                table: "TribeCurrent");

            migrationBuilder.DropColumn(
                name: "Points24",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RA24",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RO24",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "RS24",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "Ranking24",
                table: "PlayerCurrents");

            migrationBuilder.DropColumn(
                name: "VillagesCount24",
                table: "PlayerCurrents");
        }
    }
}
