using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtOnWheels.Data.Migrations
{
    /// <inheritdoc />
    public partial class Authoritymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Exhibitions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exhibitions_CustomerId",
                table: "Exhibitions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibitions_AspNetUsers_CustomerId",
                table: "Exhibitions",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibitions_AspNetUsers_CustomerId",
                table: "Exhibitions");

            migrationBuilder.DropIndex(
                name: "IX_Exhibitions_CustomerId",
                table: "Exhibitions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Exhibitions");
        }
    }
}
