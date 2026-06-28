using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class MedidasUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SacoPecho",
                table: "MedidasOrden",
                newName: "VientreSaco");

            migrationBuilder.RenameColumn(
                name: "SacoMangaIzq",
                table: "MedidasOrden",
                newName: "VientreChaleco");

            migrationBuilder.RenameColumn(
                name: "SacoMangaDer",
                table: "MedidasOrden",
                newName: "VientreCamisa");

            migrationBuilder.RenameColumn(
                name: "SacoLargoFrente",
                table: "MedidasOrden",
                newName: "TamañoInferiorChaleco");

            migrationBuilder.RenameColumn(
                name: "SacoLargoEspalda",
                table: "MedidasOrden",
                newName: "RodillaPantalon");

            migrationBuilder.RenameColumn(
                name: "SacoHombros",
                table: "MedidasOrden",
                newName: "PosicionPrimerBSaco");

            migrationBuilder.RenameColumn(
                name: "SacoEstomago",
                table: "MedidasOrden",
                newName: "PosicionPrimerBChaleco");

            migrationBuilder.RenameColumn(
                name: "SacoCadera",
                table: "MedidasOrden",
                newName: "PosicionPrimerBCamisa");

            migrationBuilder.RenameColumn(
                name: "SacoBiceps",
                table: "MedidasOrden",
                newName: "PechoSaco");

            migrationBuilder.RenameColumn(
                name: "PantTiro",
                table: "MedidasOrden",
                newName: "PechoChaleco");

            migrationBuilder.RenameColumn(
                name: "PantMuslo",
                table: "MedidasOrden",
                newName: "PechoCamisa");

            migrationBuilder.RenameColumn(
                name: "PantLargoIzq",
                table: "MedidasOrden",
                newName: "NucaCinturaSaco");

            migrationBuilder.RenameColumn(
                name: "PantLargoDer",
                table: "MedidasOrden",
                newName: "NucaCinturaChaleco");

            migrationBuilder.RenameColumn(
                name: "PantCintura",
                table: "MedidasOrden",
                newName: "NucaCinturaCamisa");

            migrationBuilder.RenameColumn(
                name: "PantCadera",
                table: "MedidasOrden",
                newName: "MuñecaSaco");

            migrationBuilder.RenameColumn(
                name: "CamisaManga",
                table: "MedidasOrden",
                newName: "MuñecaCamisa");

            migrationBuilder.RenameColumn(
                name: "CamisaCuello",
                table: "MedidasOrden",
                newName: "MusloPantalon");

            migrationBuilder.AddColumn<decimal>(
                name: "AlTerrillaPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AlturaCinturaDPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AlturaCinturaTPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AnchoTraseroCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AnchoTraseroSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AntebrazoCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AntebrazoSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BicepsCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BicepsSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BrazaletePantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaderaPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaderasCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaderasChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaderasSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CinturaPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CollarCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CollarChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CollarSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EntrepiernaPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstomagoCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstomagoChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstomagoSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HombroDelanteroCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HombroDelanteroSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HombrosCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HombrosSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudCinturaDChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudCinturaDelanteraCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudCinturaDelanteraSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudDPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudEspaldaCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudEspaldaChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudEspaldaSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudFrontalCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudFrontalChaleco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudFrontalSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudIPantalon",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudMangaDCamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudMangaDSaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudMangaICamisa",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudMangaISaco",
                table: "MedidasOrden",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlTerrillaPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AlturaCinturaDPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AlturaCinturaTPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AnchoTraseroCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AnchoTraseroSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AntebrazoCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "AntebrazoSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "BicepsCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "BicepsSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "BrazaletePantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CaderaPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CaderasCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CaderasChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CaderasSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CinturaPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CollarCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CollarChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "CollarSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "EntrepiernaPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "EstomagoCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "EstomagoChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "EstomagoSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "HombroDelanteroCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "HombroDelanteroSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "HombrosCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "HombrosSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudCinturaDChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudCinturaDelanteraCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudCinturaDelanteraSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudDPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudEspaldaCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudEspaldaChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudEspaldaSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudFrontalCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudFrontalChaleco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudFrontalSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudIPantalon",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudMangaDCamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudMangaDSaco",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudMangaICamisa",
                table: "MedidasOrden");

            migrationBuilder.DropColumn(
                name: "LongitudMangaISaco",
                table: "MedidasOrden");

            migrationBuilder.RenameColumn(
                name: "VientreSaco",
                table: "MedidasOrden",
                newName: "SacoPecho");

            migrationBuilder.RenameColumn(
                name: "VientreChaleco",
                table: "MedidasOrden",
                newName: "SacoMangaIzq");

            migrationBuilder.RenameColumn(
                name: "VientreCamisa",
                table: "MedidasOrden",
                newName: "SacoMangaDer");

            migrationBuilder.RenameColumn(
                name: "TamañoInferiorChaleco",
                table: "MedidasOrden",
                newName: "SacoLargoFrente");

            migrationBuilder.RenameColumn(
                name: "RodillaPantalon",
                table: "MedidasOrden",
                newName: "SacoLargoEspalda");

            migrationBuilder.RenameColumn(
                name: "PosicionPrimerBSaco",
                table: "MedidasOrden",
                newName: "SacoHombros");

            migrationBuilder.RenameColumn(
                name: "PosicionPrimerBChaleco",
                table: "MedidasOrden",
                newName: "SacoEstomago");

            migrationBuilder.RenameColumn(
                name: "PosicionPrimerBCamisa",
                table: "MedidasOrden",
                newName: "SacoCadera");

            migrationBuilder.RenameColumn(
                name: "PechoSaco",
                table: "MedidasOrden",
                newName: "SacoBiceps");

            migrationBuilder.RenameColumn(
                name: "PechoChaleco",
                table: "MedidasOrden",
                newName: "PantTiro");

            migrationBuilder.RenameColumn(
                name: "PechoCamisa",
                table: "MedidasOrden",
                newName: "PantMuslo");

            migrationBuilder.RenameColumn(
                name: "NucaCinturaSaco",
                table: "MedidasOrden",
                newName: "PantLargoIzq");

            migrationBuilder.RenameColumn(
                name: "NucaCinturaChaleco",
                table: "MedidasOrden",
                newName: "PantLargoDer");

            migrationBuilder.RenameColumn(
                name: "NucaCinturaCamisa",
                table: "MedidasOrden",
                newName: "PantCintura");

            migrationBuilder.RenameColumn(
                name: "MuñecaSaco",
                table: "MedidasOrden",
                newName: "PantCadera");

            migrationBuilder.RenameColumn(
                name: "MuñecaCamisa",
                table: "MedidasOrden",
                newName: "CamisaManga");

            migrationBuilder.RenameColumn(
                name: "MusloPantalon",
                table: "MedidasOrden",
                newName: "CamisaCuello");
        }
    }
}
