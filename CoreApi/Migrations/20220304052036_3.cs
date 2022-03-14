using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Player_PlayerId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TribeHistory_Tribe_TribeId",
                table: "TribeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Village_VillageId",
                table: "VillageHistory");

            migrationBuilder.DropIndex(
                name: "IX_VillageHistory_PlayerId",
                table: "VillageHistory");

            migrationBuilder.DropIndex(
                name: "IX_VillageHistory_VillageId",
                table: "VillageHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Village",
                table: "Village");

            migrationBuilder.DropIndex(
                name: "IX_TribeHistory_TribeId",
                table: "TribeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tribe",
                table: "Tribe");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHistory_PlayerId",
                table: "PlayerHistory");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHistory_TribeId",
                table: "PlayerHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "VillageHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Village",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "TribeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Tribe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "PlayerHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Village",
                table: "Village",
                columns: new[] { "Id", "WorldId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tribe",
                table: "Tribe",
                columns: new[] { "Id", "WorldId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                columns: new[] { "Id", "WorldId" });

            migrationBuilder.CreateTable(
                name: "PlayerCurrents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TribeId = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    WorldId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    VillagesCount = table.Column<int>(type: "int", nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    RA = table.Column<int>(type: "int", nullable: false),
                    RO = table.Column<int>(type: "int", nullable: false),
                    RS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCurrents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCurrents_Player_PlayerId_WorldId",
                        columns: x => new { x.PlayerId, x.WorldId },
                        principalTable: "Player",
                        principalColumns: new[] { "Id", "WorldId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCurrents_Tribe_TribeId_WorldId",
                        columns: x => new { x.TribeId, x.WorldId },
                        principalTable: "Tribe",
                        principalColumns: new[] { "Id", "WorldId" });
                });

            migrationBuilder.CreateTable(
                name: "TribeCurrent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorldId = table.Column<int>(type: "int", nullable: false),
                    TribeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RA = table.Column<int>(type: "int", nullable: false),
                    RO = table.Column<int>(type: "int", nullable: false),
                    RS = table.Column<int>(type: "int", nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TribeCurrent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TribeCurrent_Tribe_TribeId_WorldId",
                        columns: x => new { x.TribeId, x.WorldId },
                        principalTable: "Tribe",
                        principalColumns: new[] { "Id", "WorldId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillageCurrent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    WorldId = table.Column<int>(type: "int", nullable: false),
                    VillageId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageCurrent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageCurrent_Player_PlayerId_WorldId",
                        columns: x => new { x.PlayerId, x.WorldId },
                        principalTable: "Player",
                        principalColumns: new[] { "Id", "WorldId" });
                    table.ForeignKey(
                        name: "FK_VillageCurrent_Village_VillageId_WorldId",
                        columns: x => new { x.VillageId, x.WorldId },
                        principalTable: "Village",
                        principalColumns: new[] { "Id", "WorldId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillageHistory_PlayerId_WorldId",
                table: "VillageHistory",
                columns: new[] { "PlayerId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_VillageHistory_VillageId_WorldId",
                table: "VillageHistory",
                columns: new[] { "VillageId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_Village_WorldId",
                table: "Village",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_TribeHistory_TribeId_WorldId",
                table: "TribeHistory",
                columns: new[] { "TribeId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tribe_WorldId",
                table: "Tribe",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistory_PlayerId_WorldId",
                table: "PlayerHistory",
                columns: new[] { "PlayerId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistory_TribeId_WorldId",
                table: "PlayerHistory",
                columns: new[] { "TribeId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_Player_WorldId",
                table: "Player",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCurrents_PlayerId_WorldId",
                table: "PlayerCurrents",
                columns: new[] { "PlayerId", "WorldId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCurrents_TribeId_WorldId",
                table: "PlayerCurrents",
                columns: new[] { "TribeId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_TribeCurrent_TribeId_WorldId",
                table: "TribeCurrent",
                columns: new[] { "TribeId", "WorldId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageCurrent_PlayerId_WorldId",
                table: "VillageCurrent",
                columns: new[] { "PlayerId", "WorldId" });

            migrationBuilder.CreateIndex(
                name: "IX_VillageCurrent_VillageId_WorldId",
                table: "VillageCurrent",
                columns: new[] { "VillageId", "WorldId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_World_WorldId",
                table: "Player",
                column: "WorldId",
                principalTable: "World",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Player_PlayerId_WorldId",
                table: "PlayerHistory",
                columns: new[] { "PlayerId", "WorldId" },
                principalTable: "Player",
                principalColumns: new[] { "Id", "WorldId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId_WorldId",
                table: "PlayerHistory",
                columns: new[] { "TribeId", "WorldId" },
                principalTable: "Tribe",
                principalColumns: new[] { "Id", "WorldId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tribe_World_WorldId",
                table: "Tribe",
                column: "WorldId",
                principalTable: "World",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TribeHistory_Tribe_TribeId_WorldId",
                table: "TribeHistory",
                columns: new[] { "TribeId", "WorldId" },
                principalTable: "Tribe",
                principalColumns: new[] { "Id", "WorldId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Village_World_WorldId",
                table: "Village",
                column: "WorldId",
                principalTable: "World",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Player_PlayerId_WorldId",
                table: "VillageHistory",
                columns: new[] { "PlayerId", "WorldId" },
                principalTable: "Player",
                principalColumns: new[] { "Id", "WorldId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Village_VillageId_WorldId",
                table: "VillageHistory",
                columns: new[] { "VillageId", "WorldId" },
                principalTable: "Village",
                principalColumns: new[] { "Id", "WorldId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_World_WorldId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Player_PlayerId_WorldId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId_WorldId",
                table: "PlayerHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Tribe_World_WorldId",
                table: "Tribe");

            migrationBuilder.DropForeignKey(
                name: "FK_TribeHistory_Tribe_TribeId_WorldId",
                table: "TribeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Village_World_WorldId",
                table: "Village");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Player_PlayerId_WorldId",
                table: "VillageHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_VillageHistory_Village_VillageId_WorldId",
                table: "VillageHistory");

            migrationBuilder.DropTable(
                name: "PlayerCurrents");

            migrationBuilder.DropTable(
                name: "TribeCurrent");

            migrationBuilder.DropTable(
                name: "VillageCurrent");

            migrationBuilder.DropIndex(
                name: "IX_VillageHistory_PlayerId_WorldId",
                table: "VillageHistory");

            migrationBuilder.DropIndex(
                name: "IX_VillageHistory_VillageId_WorldId",
                table: "VillageHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Village",
                table: "Village");

            migrationBuilder.DropIndex(
                name: "IX_Village_WorldId",
                table: "Village");

            migrationBuilder.DropIndex(
                name: "IX_TribeHistory_TribeId_WorldId",
                table: "TribeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tribe",
                table: "Tribe");

            migrationBuilder.DropIndex(
                name: "IX_Tribe_WorldId",
                table: "Tribe");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHistory_PlayerId_WorldId",
                table: "PlayerHistory");

            migrationBuilder.DropIndex(
                name: "IX_PlayerHistory_TribeId_WorldId",
                table: "PlayerHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_WorldId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "VillageHistory");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Village");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "TribeHistory");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Tribe");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "PlayerHistory");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Player");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Village",
                table: "Village",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tribe",
                table: "Tribe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VillageHistory_PlayerId",
                table: "VillageHistory",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageHistory_VillageId",
                table: "VillageHistory",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_TribeHistory_TribeId",
                table: "TribeHistory",
                column: "TribeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistory_PlayerId",
                table: "PlayerHistory",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistory_TribeId",
                table: "PlayerHistory",
                column: "TribeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Player_PlayerId",
                table: "PlayerHistory",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHistory_Tribe_TribeId",
                table: "PlayerHistory",
                column: "TribeId",
                principalTable: "Tribe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TribeHistory_Tribe_TribeId",
                table: "TribeHistory",
                column: "TribeId",
                principalTable: "Tribe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Player_PlayerId",
                table: "VillageHistory",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillageHistory_Village_VillageId",
                table: "VillageHistory",
                column: "VillageId",
                principalTable: "Village",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
