using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "VillageHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TribeId",
                table: "PlayerHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory",
                column: "TribeId",
                principalTable: "Tribe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "VillageHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TribeId",
                table: "PlayerHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory",
                column: "TribeId",
                principalTable: "Tribe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
