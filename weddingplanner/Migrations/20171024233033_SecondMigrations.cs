using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace weddingplanner.Migrations
{
    public partial class SecondMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Weddings_WeddingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WeddingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WeddingId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Rsvps",
                columns: table => new
                {
                    RsvpId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    WeddingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rsvps", x => x.RsvpId);
                    table.ForeignKey(
                        name: "FK_Rsvps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rsvps_Weddings_WeddingId",
                        column: x => x.WeddingId,
                        principalTable: "Weddings",
                        principalColumn: "WeddingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rsvps_UserId",
                table: "Rsvps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rsvps_WeddingId",
                table: "Rsvps",
                column: "WeddingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rsvps");

            migrationBuilder.AddColumn<int>(
                name: "WeddingId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WeddingId",
                table: "Users",
                column: "WeddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Weddings_WeddingId",
                table: "Users",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
