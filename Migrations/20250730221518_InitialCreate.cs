using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaMuzik.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calma_listeleri",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calma_listeleri", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sanatcilar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kurulus_tarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanatcilar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "albumler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cikis_tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sanatci_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albumler", x => x.id);
                    table.ForeignKey(
                        name: "FK_albumler_sanatcilar_sanatci_id",
                        column: x => x.sanatci_id,
                        principalTable: "sanatcilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sarkilar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    album_id = table.Column<int>(type: "int", nullable: false),
                    sanatci_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sarkilar", x => x.id);
                    table.ForeignKey(
                        name: "FK_sarkilar_albumler_album_id",
                        column: x => x.album_id,
                        principalTable: "albumler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sarkilar_sanatcilar_sanatci_id",
                        column: x => x.sanatci_id,
                        principalTable: "sanatcilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "calma_listeleri_sarkilari",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calma_listesi_id = table.Column<int>(type: "int", nullable: false),
                    sarki_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calma_listeleri_sarkilari", x => x.id);
                    table.ForeignKey(
                        name: "FK_calma_listeleri_sarkilari_calma_listeleri_calma_listesi_id",
                        column: x => x.calma_listesi_id,
                        principalTable: "calma_listeleri",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_calma_listeleri_sarkilari_sarkilar_sarki_id",
                        column: x => x.sarki_id,
                        principalTable: "sarkilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_albumler_sanatci_id",
                table: "albumler",
                column: "sanatci_id");

            migrationBuilder.CreateIndex(
                name: "IX_calma_listeleri_sarkilari_calma_listesi_id",
                table: "calma_listeleri_sarkilari",
                column: "calma_listesi_id");

            migrationBuilder.CreateIndex(
                name: "IX_calma_listeleri_sarkilari_sarki_id",
                table: "calma_listeleri_sarkilari",
                column: "sarki_id");

            migrationBuilder.CreateIndex(
                name: "IX_sarkilar_album_id",
                table: "sarkilar",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_sarkilar_sanatci_id",
                table: "sarkilar",
                column: "sanatci_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calma_listeleri_sarkilari");

            migrationBuilder.DropTable(
                name: "calma_listeleri");

            migrationBuilder.DropTable(
                name: "sarkilar");

            migrationBuilder.DropTable(
                name: "albumler");

            migrationBuilder.DropTable(
                name: "sanatcilar");
        }
    }
}
