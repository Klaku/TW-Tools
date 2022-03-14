using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TribeId",
                table: "VillageCurrent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageCurrent_TribeId_WorldId",
                table: "VillageCurrent",
                columns: new[] { "TribeId", "WorldId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VillageCurrent_Tribe_TribeId_WorldId",
                table: "VillageCurrent",
                columns: new[] { "TribeId", "WorldId" },
                principalTable: "Tribe",
                principalColumns: new[] { "Id", "WorldId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillageCurrent_Tribe_TribeId_WorldId",
                table: "VillageCurrent");

            migrationBuilder.DropIndex(
                name: "IX_VillageCurrent_TribeId_WorldId",
                table: "VillageCurrent");

            migrationBuilder.DropColumn(
                name: "TribeId",
                table: "VillageCurrent");
        }
    }
}
