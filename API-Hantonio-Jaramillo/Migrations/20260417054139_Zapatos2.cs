using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class Zapatos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LargoPie",
                table: "MedidasOrden",
                newName: "LargoPieZapato");

            migrationBuilder.RenameColumn(
                name: "AnchoEmpeine",
                table: "MedidasOrden",
                newName: "AnchoEmpeineZapato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LargoPieZapato",
                table: "MedidasOrden",
                newName: "LargoPie");

            migrationBuilder.RenameColumn(
                name: "AnchoEmpeineZapato",
                table: "MedidasOrden",
                newName: "AnchoEmpeine");
        }
    }
}
