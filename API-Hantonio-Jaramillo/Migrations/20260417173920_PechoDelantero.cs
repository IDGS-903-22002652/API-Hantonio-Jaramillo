using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class PechoDelantero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PechoDelanteroCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PechoDelanteroChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PechoDelanteroSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PechoDelanteroCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "PechoDelanteroChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "PechoDelanteroSaco",
                table: "MedidasOrden");
        }
    }
}
