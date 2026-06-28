using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class fe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_TipoTraje_IdTipoTraje",
                table: "Orden");

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoTraje",
                table: "Orden",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_TipoTraje_IdTipoTraje",
                table: "Orden",
                column: "IdTipoTraje",
                principalTable: "TipoTraje",
                principalColumn: "IdTipoTraje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_TipoTraje_IdTipoTraje",
                table: "Orden");

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoTraje",
                table: "Orden",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_TipoTraje_IdTipoTraje",
                table: "Orden",
                column: "IdTipoTraje",
                principalTable: "TipoTraje",
                principalColumn: "IdTipoTraje",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
