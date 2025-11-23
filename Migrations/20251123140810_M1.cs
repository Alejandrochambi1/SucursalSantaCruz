using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sucursal.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    CodigoSucursal = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Ciudad = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    HoraApertura = table.Column<int>(type: "integer", nullable: false),
                    HoraCierre = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.CodigoSucursal);
                });

            migrationBuilder.CreateTable(
                name: "GerentesSucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoSucursal = table.Column<string>(type: "text", nullable: false),
                    CI = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GerentesSucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GerentesSucursal_Sucursales_CodigoSucursal",
                        column: x => x.CodigoSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "CodigoSucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportesDiarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoSucursal = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalVentasProductos = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVentasServicios = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoLaboralDia = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoOperativoDia = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoInventarioDia = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoMarketingDia = table.Column<decimal>(type: "numeric", nullable: false),
                    PorcentajeUtilidadBruta = table.Column<decimal>(type: "numeric", nullable: false),
                    StockValorado = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductosVencidos = table.Column<int>(type: "integer", nullable: false),
                    ProductosDevueltos = table.Column<int>(type: "integer", nullable: false),
                    CantidadTransacciones = table.Column<int>(type: "integer", nullable: false),
                    CantidadProductosVendidos = table.Column<int>(type: "integer", nullable: false),
                    CantidadClientes = table.Column<int>(type: "integer", nullable: false),
                    CantidadEmpleadosPresentes = table.Column<int>(type: "integer", nullable: false),
                    CantidadEmpleadosAusentes = table.Column<int>(type: "integer", nullable: false),
                    NominaDelDia = table.Column<decimal>(type: "numeric", nullable: false),
                    PresupuestoGastadoMarketing = table.Column<decimal>(type: "numeric", nullable: false),
                    CampanasActivas = table.Column<int>(type: "integer", nullable: false),
                    EstadoConsolidacion = table.Column<string>(type: "text", nullable: false),
                    FechaConsolidacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DetallesJSON = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesDiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportesDiarios_Sucursales_CodigoSucursal",
                        column: x => x.CodigoSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "CodigoSucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesInternas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoSucursal = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    MontoSolicitado = table.Column<decimal>(type: "numeric", nullable: true),
                    CISolicitante = table.Column<string>(type: "text", nullable: false),
                    CIAprobador = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaAprobacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCompletacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MotivoRechazo = table.Column<string>(type: "text", nullable: false),
                    DetallesJSON = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesInternas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesInternas_Sucursales_CodigoSucursal",
                        column: x => x.CodigoSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "CodigoSucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSolicitud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSolicitud = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoArticulo = table.Column<string>(type: "text", nullable: false),
                    NombreArticulo = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSolicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsSolicitud_SolicitudesInternas_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudesInternas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GerentesSucursal_CodigoSucursal",
                table: "GerentesSucursal",
                column: "CodigoSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSolicitud_IdSolicitud",
                table: "ItemsSolicitud",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDiarios_CodigoSucursal_Fecha",
                table: "ReportesDiarios",
                columns: new[] { "CodigoSucursal", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesInternas_CodigoSucursal_Estado",
                table: "SolicitudesInternas",
                columns: new[] { "CodigoSucursal", "Estado" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GerentesSucursal");

            migrationBuilder.DropTable(
                name: "ItemsSolicitud");

            migrationBuilder.DropTable(
                name: "ReportesDiarios");

            migrationBuilder.DropTable(
                name: "SolicitudesInternas");

            migrationBuilder.DropTable(
                name: "Sucursales");
        }
    }
}
