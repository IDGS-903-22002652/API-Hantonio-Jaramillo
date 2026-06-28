using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class NumeroProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroProduccion",
                table: "DetalleZapatos",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroProduccion",
                table: "DetalleSaco",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroProduccion",
                table: "DetallePantalon",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroProduccion",
                table: "DetalleChaleco",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroProduccion",
                table: "DetalleCamisa",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroProduccion",
                table: "DetalleZapatos");

            migrationBuilder.DropColumn(
                name: "NumeroProduccion",
                table: "DetalleSaco");

            migrationBuilder.DropColumn(
                name: "NumeroProduccion",
                table: "DetallePantalon");

            migrationBuilder.DropColumn(
                name: "NumeroProduccion",
                table: "DetalleChaleco");

            migrationBuilder.DropColumn(
                name: "NumeroProduccion",
                table: "DetalleCamisa");
        }
    }
}
