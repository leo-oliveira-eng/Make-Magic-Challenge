using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Make.Magic.Challenge.Infra.Migrations
{
    public partial class Migration_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House",
                table: "Character");

            migrationBuilder.AddColumn<long>(
                name: "HouseId",
                table: "Character",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterHouse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    HouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterHouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterHouse_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_HouseId",
                table: "Character",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterHouse_HouseId",
                table: "CharacterHouse",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_CharacterHouse_HouseId",
                table: "Character",
                column: "HouseId",
                principalTable: "CharacterHouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_CharacterHouse_HouseId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "CharacterHouse");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropIndex(
                name: "IX_Character_HouseId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Character");

            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "Character",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
