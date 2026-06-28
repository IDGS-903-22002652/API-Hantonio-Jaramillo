using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class Incluye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncluyeCamisa",
                table: "Orden",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncluyeZapato",
                table: "Orden",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "esSmoking3Piezas",
                table: "Orden",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncluyeCamisa",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "IncluyeZapato",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "esSmoking3Piezas",
                table: "Orden");
        }
    }
}
