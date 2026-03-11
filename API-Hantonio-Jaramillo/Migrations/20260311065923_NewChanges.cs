using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "EstatusOrden",
                columns: table => new
                {
                    IdEstatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusOrden", x => x.IdEstatus);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TipoTraje",
                columns: table => new
                {
                    IdTipoTraje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTraje", x => x.IdTipoTraje);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCamisa",
                columns: table => new
                {
                    IdDetalleCamisa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    OpcionCamisa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoTela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloCuello = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContrasteTela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloTapeta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloPuno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsillo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlieguesFrontales = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iniciales = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioCamisa = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCamisa", x => x.IdDetalleCamisa);
                });

            migrationBuilder.CreateTable(
                name: "DetalleChaleco",
                columns: table => new
                {
                    IdDetalleChaleco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    CodigoTela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoBoton = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloCuello = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBotones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloPecho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloInf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TerminacionInf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioChaleco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleChaleco", x => x.IdDetalleChaleco);
                });

            migrationBuilder.CreateTable(
                name: "DetallePantalon",
                columns: table => new
                {
                    IdDetallePantalon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    CodigoTela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoBoton = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloPretina = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AjusteCintura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AlturaPretina = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloPliegues = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloReloj = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBajos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioPantalon = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePantalon", x => x.IdDetallePantalon);
                });

            migrationBuilder.CreateTable(
                name: "DetalleSaco",
                columns: table => new
                {
                    IdDetalleSaco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    CodigoTela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoForro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoBoton = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBotones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloSolapa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TamanoSolapa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloPecho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloInf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloBolsilloTicket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloOjalIzquierdo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstiloOjalDerecho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Monograma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioSaco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleSaco", x => x.IdDetalleSaco);
                });

            migrationBuilder.CreateTable(
                name: "MedidasOrden",
                columns: table => new
                {
                    IdMedida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TallaZapato = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TipoFit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SacoLargoFrente = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoLargoEspalda = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoHombros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoPecho = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoEstomago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoMangaIzq = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoMangaDer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoBiceps = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SacoCadera = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantLargoIzq = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantLargoDer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantCintura = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantCadera = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantMuslo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PantTiro = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CamisaCuello = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CamisaManga = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedidasOrden", x => x.IdMedida);
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    IdTipoTraje = table.Column<int>(type: "int", nullable: false),
                    IdEstatus = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCitaMedidas = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEventoEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoAbonado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_Orden_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orden_EstatusOrden_IdEstatus",
                        column: x => x.IdEstatus,
                        principalTable: "EstatusOrden",
                        principalColumn: "IdEstatus",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orden_TipoTraje_IdTipoTraje",
                        column: x => x.IdTipoTraje,
                        principalTable: "TipoTraje",
                        principalColumn: "IdTipoTraje",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    IdSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.IdSucursal);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    UltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimoLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IdSucursal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EstatusOrden",
                columns: new[] { "IdEstatus", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Pendiente de medidas" },
                    { 2, "Toma de medidas" },
                    { 3, "En confección" },
                    { 4, "Entregado" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Empleado" }
                });

            migrationBuilder.InsertData(
                table: "Sucursal",
                columns: new[] { "IdSucursal", "Direccion", "Estatus", "IdUsuario", "Nombre", "Telefono" },
                values: new object[] { 1, "C. del Fuego 226 A, Jardines del Moral, 37160 León de los Aldama, Gto.", true, null, "Sucursal Jardines del Moral", "477 799 3177" });

            migrationBuilder.InsertData(
                table: "TipoTraje",
                columns: new[] { "IdTipoTraje", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Dos piezas" },
                    { 2, "Tres piezas" },
                    { 3, "Saco" },
                    { 4, "Pantalón" },
                    { 5, "Chaleco" },
                    { 6, "Camisa" },
                    { 7, "Frac" },
                    { 8, "Chaque" },
                    { 9, "Smoking" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCamisa_IdOrden",
                table: "DetalleCamisa",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleChaleco_IdOrden",
                table: "DetalleChaleco",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePantalon_IdOrden",
                table: "DetallePantalon",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSaco_IdOrden",
                table: "DetalleSaco",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedidasOrden_IdOrden",
                table: "MedidasOrden",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdCliente",
                table: "Orden",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdEstatus",
                table: "Orden",
                column: "IdEstatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdSucursal",
                table: "Orden",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdTipoTraje",
                table: "Orden",
                column: "IdTipoTraje");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdUsuario",
                table: "Orden",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_IdUsuario",
                table: "Sucursal",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdSucursal",
                table: "Usuario",
                column: "IdSucursal");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCamisa_Orden_IdOrden",
                table: "DetalleCamisa",
                column: "IdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleChaleco_Orden_IdOrden",
                table: "DetalleChaleco",
                column: "IdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePantalon_Orden_IdOrden",
                table: "DetallePantalon",
                column: "IdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleSaco_Orden_IdOrden",
                table: "DetalleSaco",
                column: "IdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedidasOrden_Orden_IdOrden",
                table: "MedidasOrden",
                column: "IdOrden",
                principalTable: "Orden",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Sucursal_IdSucursal",
                table: "Orden",
                column: "IdSucursal",
                principalTable: "Sucursal",
                principalColumn: "IdSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Usuario_IdUsuario",
                table: "Orden",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursal_Usuario_IdUsuario",
                table: "Sucursal",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Sucursal_IdSucursal",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "DetalleCamisa");

            migrationBuilder.DropTable(
                name: "DetalleChaleco");

            migrationBuilder.DropTable(
                name: "DetallePantalon");

            migrationBuilder.DropTable(
                name: "DetalleSaco");

            migrationBuilder.DropTable(
                name: "MedidasOrden");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "EstatusOrden");

            migrationBuilder.DropTable(
                name: "TipoTraje");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
