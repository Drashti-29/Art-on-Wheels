using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtOnWheels.Data.Migrations
{
    /// <inheritdoc />
    public partial class initcreations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    ArtworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationYear = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_Artworks_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artworks_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "CarId");
                });

            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: table => new
                {
                    ExhibitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExhibitionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.ExhibitionId);
                    table.ForeignKey(
                        name: "FK_Exhibitions_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "CarId");
                });

            migrationBuilder.CreateTable(
                name: "ArtworkExhibition",
                columns: table => new
                {
                    ArtworksArtworkId = table.Column<int>(type: "int", nullable: false),
                    ExhibitionsExhibitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkExhibition", x => new { x.ArtworksArtworkId, x.ExhibitionsExhibitionId });
                    table.ForeignKey(
                        name: "FK_ArtworkExhibition_Artworks_ArtworksArtworkId",
                        column: x => x.ArtworksArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtworkExhibition_Exhibitions_ExhibitionsExhibitionId",
                        column: x => x.ExhibitionsExhibitionId,
                        principalTable: "Exhibitions",
                        principalColumn: "ExhibitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExhibitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staff_Exhibitions_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibitions",
                        principalColumn: "ExhibitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkExhibition_ExhibitionsExhibitionId",
                table: "ArtworkExhibition",
                column: "ExhibitionsExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ArtistId",
                table: "Artworks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_CarId",
                table: "Artworks",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Exhibitions_CarId",
                table: "Exhibitions",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ExhibitionId",
                table: "Staff",
                column: "ExhibitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtworkExhibition");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "Exhibitions");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Car");
        }
    }
}
