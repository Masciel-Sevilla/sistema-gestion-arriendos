using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArriendosApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edificio",
                columns: table => new
                {
                    IdEdificio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edificio", x => x.IdEdificio);
                });

            migrationBuilder.CreateTable(
                name: "Inquilino",
                columns: table => new
                {
                    IdInquilino = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: false),
                    Identificacion = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquilino", x => x.IdInquilino);
                });

            migrationBuilder.CreateTable(
                name: "Inmueble",
                columns: table => new
                {
                    IdInmueble = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEdificio = table.Column<int>(type: "integer", nullable: false),
                    NumeroDepa = table.Column<string>(type: "text", nullable: false),
                    NumeroMedidorLuz = table.Column<string>(type: "text", nullable: false),
                    NumeroMedidorAgua = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    EdificioIdEdificio = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inmueble", x => x.IdInmueble);
                    table.ForeignKey(
                        name: "FK_Inmueble_Edificio_EdificioIdEdificio",
                        column: x => x.EdificioIdEdificio,
                        principalTable: "Edificio",
                        principalColumn: "IdEdificio");
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    IdContrato = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdInmueble = table.Column<int>(type: "integer", nullable: false),
                    IdInquilino = table.Column<int>(type: "integer", nullable: false),
                    MontoArriendo = table.Column<decimal>(type: "numeric", nullable: false),
                    MontoGarantia = table.Column<decimal>(type: "numeric", nullable: false),
                    DiaPagoMensual = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EsActivo = table.Column<bool>(type: "boolean", nullable: false),
                    InquilinoIdInquilino = table.Column<int>(type: "integer", nullable: true),
                    InmuebleIdInmueble = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.IdContrato);
                    table.ForeignKey(
                        name: "FK_Contrato_Inmueble_InmuebleIdInmueble",
                        column: x => x.InmuebleIdInmueble,
                        principalTable: "Inmueble",
                        principalColumn: "IdInmueble");
                    table.ForeignKey(
                        name: "FK_Contrato_Inquilino_InquilinoIdInquilino",
                        column: x => x.InquilinoIdInquilino,
                        principalTable: "Inquilino",
                        principalColumn: "IdInquilino");
                });

            migrationBuilder.CreateTable(
                name: "CobroMensual",
                columns: table => new
                {
                    IdCobro = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdContrato = table.Column<int>(type: "integer", nullable: false),
                    Mes = table.Column<int>(type: "integer", nullable: false),
                    Anio = table.Column<int>(type: "integer", nullable: false),
                    ValorArriendo = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorAgua = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorLuz = table.Column<decimal>(type: "numeric", nullable: false),
                    SaldoAnterior = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalPagar = table.Column<decimal>(type: "numeric", nullable: false),
                    MontoPagado = table.Column<decimal>(type: "numeric", nullable: false),
                    SaldoPendiente = table.Column<decimal>(type: "numeric", nullable: false),
                    EsPagado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaUltimoPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContratoIdContrato = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobroMensual", x => x.IdCobro);
                    table.ForeignKey(
                        name: "FK_CobroMensual_Contrato_ContratoIdContrato",
                        column: x => x.ContratoIdContrato,
                        principalTable: "Contrato",
                        principalColumn: "IdContrato");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobroMensual_ContratoIdContrato",
                table: "CobroMensual",
                column: "ContratoIdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InmuebleIdInmueble",
                table: "Contrato",
                column: "InmuebleIdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InquilinoIdInquilino",
                table: "Contrato",
                column: "InquilinoIdInquilino");

            migrationBuilder.CreateIndex(
                name: "IX_Inmueble_EdificioIdEdificio",
                table: "Inmueble",
                column: "EdificioIdEdificio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobroMensual");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Inmueble");

            migrationBuilder.DropTable(
                name: "Inquilino");

            migrationBuilder.DropTable(
                name: "Edificio");
        }
    }
}
