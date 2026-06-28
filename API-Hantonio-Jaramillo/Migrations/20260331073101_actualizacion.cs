using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class actualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdenIdOrden",
                table: "MedidasOrden",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdenIdOrden",
                table: "DetalleSaco",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdenIdOrden",
                table: "DetallePantalon",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdenIdOrden",
                table: "DetalleChaleco",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdenIdOrden",
                table: "DetalleCamisa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedidasOrden_OrdenIdOrden",
                table: "MedidasOrden",
                column: "OrdenIdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSaco_OrdenIdOrden",
                table: "DetalleSaco",
                column: "OrdenIdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePantalon_OrdenIdOrden",
                table: "DetallePantalon",
                column: "OrdenIdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleChaleco_OrdenIdOrden",
                table: "DetalleChaleco",
                column: "OrdenIdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCamisa_OrdenIdOrden",
                table: "DetalleCamisa",
                column: "OrdenIdOrden");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCamisa_Orden_OrdenIdOrden",
                table: "DetalleCamisa",
                column: "OrdenIdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleChaleco_Orden_OrdenIdOrden",
                table: "DetalleChaleco",
                column: "OrdenIdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePantalon_Orden_OrdenIdOrden",
                table: "DetallePantalon",
                column: "OrdenIdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleSaco_Orden_OrdenIdOrden",
                table: "DetalleSaco",
                column: "OrdenIdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden");

            migrationBuilder.AddForeignKey(
                name: "FK_MedidasOrden_Orden_OrdenIdOrden",
                table: "MedidasOrden",
                column: "OrdenIdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCamisa_Orden_OrdenIdOrden",
                table: "DetalleCamisa");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleChaleco_Orden_OrdenIdOrden",
                table: "DetalleChaleco");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePantalon_Orden_OrdenIdOrden",
                table: "DetallePantalon");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleSaco_Orden_OrdenIdOrden",
                table: "DetalleSaco");

            migrationBuilder.DropForeignKey(
                name: "FK_MedidasOrden_Orden_OrdenIdOrden",
                table: "MedidasOrden");

            migrationBuilder.DropIndex(
                name: "IX_MedidasOrden_OrdenIdOrden",
                table: "MedidasOrden");

            migrationBuilder.DropIndex(
                name: "IX_DetalleSaco_OrdenIdOrden",
                table: "DetalleSaco");

            migrationBuilder.DropIndex(
                name: "IX_DetallePantalon_OrdenIdOrden",
                table: "DetallePantalon");

            migrationBuilder.DropIndex(
                name: "IX_DetalleChaleco_OrdenIdOrden",
                table: "DetalleChaleco");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCamisa_OrdenIdOrden",
                table: "DetalleCamisa");

            migrationBuilder.DropColumn(
                name: "OrdenIdOrden",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "OrdenIdOrden",
                table: "DetalleSaco");

            migrationBuilder.DropColumn(
                name: "OrdenIdOrden",
                table: "DetallePantalon");

            migrationBuilder.DropColumn(
                name: "OrdenIdOrden",
                table: "DetalleChaleco");

            migrationBuilder.DropColumn(
                name: "OrdenIdOrden",
                table: "DetalleCamisa");
        }
    }
}
