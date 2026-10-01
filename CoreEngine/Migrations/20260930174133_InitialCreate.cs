using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreEngine.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetComponents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ParentID = table.Column<string>(type: "text", nullable: true),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    LastInspectTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetComponents_AssetComponents_ParentID",
                        column: x => x.ParentID,
                        principalTable: "AssetComponents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentHotspots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetComponentID = table.Column<string>(type: "text", nullable: false),
                    DocumentID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CoordX = table.Column<int>(type: "integer", nullable: false),
                    CoordY = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentHotspots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentHotspots_AssetComponents_AssetComponentID",
                        column: x => x.AssetComponentID,
                        principalTable: "AssetComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetComponents_ParentID",
                table: "AssetComponents",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHotspots_AssetComponentID",
                table: "DocumentHotspots",
                column: "AssetComponentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentHotspots");

            migrationBuilder.DropTable(
                name: "AssetComponents");
        }
    }
}
