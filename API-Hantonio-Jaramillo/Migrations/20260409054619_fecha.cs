using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class fecha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "Orden",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 1,
                column: "Descripcion",
                value: "Nueva orden");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 2,
                column: "Descripcion",
                value: "En revisión");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 3,
                column: "Descripcion",
                value: "Pendiente de medidas");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 4,
                column: "Descripcion",
                value: "Medidas registradas");

            migrationBuilder.InsertData(
                table: "EstatusOrden",
                columns: new[] { "IdEstatus", "Descripcion" },
                values: new object[,]
                {
                    { 5, "Autorizado para producción" },
                    { 6, "En confección" },
                    { 7, "Listo para entrega" },
                    { 8, "Entregado" },
                    { 9, "Cancelado" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "Orden");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 1,
                column: "Descripcion",
                value: "Pendiente de medidas");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 2,
                column: "Descripcion",
                value: "Toma de medidas");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 3,
                column: "Descripcion",
                value: "En confección");

            migrationBuilder.UpdateData(
                table: "EstatusOrden",
                keyColumn: "IdEstatus",
                keyValue: 4,
                column: "Descripcion",
                value: "Entregado");
        }
    }
}
