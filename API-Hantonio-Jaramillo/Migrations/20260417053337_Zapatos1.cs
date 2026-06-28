using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    public partial class Zapatos1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnchoEmpeine",
                table: "DetalleZapatos");

            migrationBuilder.DropColumn(
                name: "LargoPie",
                table: "DetalleZapatos");

            migrationBuilder.DropColumn(
                name: "TallaZapato",
                table: "DetalleZapatos");

            migrationBuilder.AddColumn<decimal>(
                name: "AnchoEmpeine",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LargoPie",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TallaZapato",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnchoEmpeine",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LargoPie",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "TallaZapato",
                table: "MedidasOrden");

            migrationBuilder.AddColumn<decimal>(
                name: "AnchoEmpeine",
                table: "DetalleZapatos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LargoPie",
                table: "DetalleZapatos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TallaZapato",
                table: "DetalleZapatos",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
