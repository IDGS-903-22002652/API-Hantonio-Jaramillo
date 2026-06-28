using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class Zapatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TallaZapato",
                table: "MedidasOrden");

            migrationBuilder.CreateTable(
                name: "DetalleZapatos",
                columns: table => new
                {
                    IdDetalleZapato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    EstiloZapato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TallaZapato = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AnchoEmpeine = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LargoPie = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioZapato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrdenIdOrden = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleZapatos", x => x.IdDetalleZapato);
                    table.ForeignKey(
                        name: "FK_DetalleZapatos_Orden_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Orden",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleZapatos_Orden_OrdenIdOrden",
                        column: x => x.OrdenIdOrden,
                        principalTable: "Orden",
                        principalColumn: "IdOrden");
                });

            migrationBuilder.InsertData(
                table: "TipoTraje",
                columns: new[] { "IdTipoTraje", "Descripcion" },
                values: new object[] { 10, "Zapatos" });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleZapatos_IdOrden",
                table: "DetalleZapatos",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleZapatos_OrdenIdOrden",
                table: "DetalleZapatos",
                column: "OrdenIdOrden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleZapatos");

            migrationBuilder.DeleteData(
                table: "TipoTraje",
                keyColumn: "IdTipoTraje",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "TallaZapato",
                table: "MedidasOrden",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
