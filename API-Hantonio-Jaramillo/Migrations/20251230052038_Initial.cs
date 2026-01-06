using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hantonio_Jaramillo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Contrasena",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "CLIENTE");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "CLIENTE",
                newName: "nombre_completo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CLIENTE",
                newName: "id_cliente");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_completo",
                table: "CLIENTE",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ciudad",
                table: "CLIENTE",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "CLIENTE",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "CLIENTE",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_registro",
                table: "CLIENTE",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "telefono",
                table: "CLIENTE",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CLIENTE",
                table: "CLIENTE",
                column: "id_cliente");

            migrationBuilder.CreateTable(
                name: "CAT_ESTATUS",
                columns: table => new
                {
                    id_estatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ESTATUS", x => x.id_estatus);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RECURSOS_DISENO",
                columns: table => new
                {
                    id_recurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prenda = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    atributo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    codigo_valor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombre_mostrar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    url_imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RECURSOS_DISENO", x => x.id_recurso);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TIPO_TRAJE",
                columns: table => new
                {
                    id_tipo_traje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TIPO_TRAJE", x => x.id_tipo_traje);
                });

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "SUCURSAL",
                columns: table => new
                {
                    id_sucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    encargado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUCURSAL", x => x.id_sucursal);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_rol = table.Column<int>(type: "int", nullable: true),
                    id_sucursal = table.Column<int>(type: "int", nullable: true),
                    nombre_completo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    ultimo_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ultimo_logout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_USUARIO_ROL_id_rol",
                        column: x => x.id_rol,
                        principalTable: "ROL",
                        principalColumn: "id_rol");
                    table.ForeignKey(
                        name: "FK_USUARIO_SUCURSAL_id_sucursal",
                        column: x => x.id_sucursal,
                        principalTable: "SUCURSAL",
                        principalColumn: "id_sucursal");
                });

            migrationBuilder.CreateTable(
                name: "LOG_ACCESOS",
                columns: table => new
                {
                    id_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_salida = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOG_ACCESOS", x => x.id_log);
                    table.ForeignKey(
                        name: "FK_LOG_ACCESOS_USUARIO_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "USUARIO",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "ORDEN",
                columns: table => new
                {
                    id_orden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cliente = table.Column<int>(type: "int", nullable: true),
                    id_usuario_creador = table.Column<int>(type: "int", nullable: true),
                    id_sucursal = table.Column<int>(type: "int", nullable: true),
                    id_tipo_traje = table.Column<int>(type: "int", nullable: true),
                    id_estatus = table.Column<int>(type: "int", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_cita_medidas = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fecha_evento_entrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    costo_total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    monto_abonado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    incluye_camisa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDEN", x => x.id_orden);
                    table.ForeignKey(
                        name: "FK_ORDEN_CAT_ESTATUS_id_estatus",
                        column: x => x.id_estatus,
                        principalTable: "CAT_ESTATUS",
                        principalColumn: "id_estatus");
                    table.ForeignKey(
                        name: "FK_ORDEN_CAT_TIPO_TRAJE_id_tipo_traje",
                        column: x => x.id_tipo_traje,
                        principalTable: "CAT_TIPO_TRAJE",
                        principalColumn: "id_tipo_traje");
                    table.ForeignKey(
                        name: "FK_ORDEN_CLIENTE_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "CLIENTE",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK_ORDEN_SUCURSAL_id_sucursal",
                        column: x => x.id_sucursal,
                        principalTable: "SUCURSAL",
                        principalColumn: "id_sucursal");
                    table.ForeignKey(
                        name: "FK_ORDEN_USUARIO_id_usuario_creador",
                        column: x => x.id_usuario_creador,
                        principalTable: "USUARIO",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "DETALLE_CAMISA",
                columns: table => new
                {
                    id_detalle_camisa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_orden = table.Column<int>(type: "int", nullable: true),
                    codigo_tela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estilo_cuello = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estilo_puno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    iniciales = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLE_CAMISA", x => x.id_detalle_camisa);
                    table.ForeignKey(
                        name: "FK_DETALLE_CAMISA_ORDEN_id_orden",
                        column: x => x.id_orden,
                        principalTable: "ORDEN",
                        principalColumn: "id_orden");
                });

            migrationBuilder.CreateTable(
                name: "DETALLE_SACO",
                columns: table => new
                {
                    id_detalle_saco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_orden = table.Column<int>(type: "int", nullable: true),
                    codigo_tela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estilo_solapa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estilo_bolsillo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    codigo_boton = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    monograma = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLE_SACO", x => x.id_detalle_saco);
                    table.ForeignKey(
                        name: "FK_DETALLE_SACO_ORDEN_id_orden",
                        column: x => x.id_orden,
                        principalTable: "ORDEN",
                        principalColumn: "id_orden");
                });

            migrationBuilder.CreateTable(
                name: "MEDIDAS_ORDEN",
                columns: table => new
                {
                    id_medida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_orden = table.Column<int>(type: "int", nullable: false),
                    s_hombros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    s_pecho = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    s_estomago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    s_largo_frente = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_cintura = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_cadera = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_tiro = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_largo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    c_cuello = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    c_manga = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    observaciones_medidas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDIDAS_ORDEN", x => x.id_medida);
                    table.ForeignKey(
                        name: "FK_MEDIDAS_ORDEN_ORDEN_id_orden",
                        column: x => x.id_orden,
                        principalTable: "ORDEN",
                        principalColumn: "id_orden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_CAMISA_id_orden",
                table: "DETALLE_CAMISA",
                column: "id_orden",
                unique: true,
                filter: "[id_orden] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_SACO_id_orden",
                table: "DETALLE_SACO",
                column: "id_orden",
                unique: true,
                filter: "[id_orden] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LOG_ACCESOS_id_usuario",
                table: "LOG_ACCESOS",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_MEDIDAS_ORDEN_id_orden",
                table: "MEDIDAS_ORDEN",
                column: "id_orden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDEN_id_cliente",
                table: "ORDEN",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_ORDEN_id_estatus",
                table: "ORDEN",
                column: "id_estatus");

            migrationBuilder.CreateIndex(
                name: "IX_ORDEN_id_sucursal",
                table: "ORDEN",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_ORDEN_id_tipo_traje",
                table: "ORDEN",
                column: "id_tipo_traje");

            migrationBuilder.CreateIndex(
                name: "IX_ORDEN_id_usuario_creador",
                table: "ORDEN",
                column: "id_usuario_creador");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_id_rol",
                table: "USUARIO",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_id_sucursal",
                table: "USUARIO",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_login",
                table: "USUARIO",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_RECURSOS_DISENO");

            migrationBuilder.DropTable(
                name: "DETALLE_CAMISA");

            migrationBuilder.DropTable(
                name: "DETALLE_SACO");

            migrationBuilder.DropTable(
                name: "LOG_ACCESOS");

            migrationBuilder.DropTable(
                name: "MEDIDAS_ORDEN");

            migrationBuilder.DropTable(
                name: "ORDEN");

            migrationBuilder.DropTable(
                name: "CAT_ESTATUS");

            migrationBuilder.DropTable(
                name: "CAT_TIPO_TRAJE");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropTable(
                name: "SUCURSAL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CLIENTE",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ciudad",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "email",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "fecha_registro",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "telefono",
                table: "CLIENTE");

            migrationBuilder.RenameTable(
                name: "CLIENTE",
                newName: "Clientes");

            migrationBuilder.RenameColumn(
                name: "nombre_completo",
                table: "Clientes",
                newName: "NombreCompleto");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Contrasena",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");
        }
    }
}
