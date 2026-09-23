using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArriendosApi.Migrations
{
    /// <inheritdoc />
    public partial class RestrictEliminacionInmueble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobroMensual_Contrato_ContratoIdContrato",
                table: "CobroMensual");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Inmueble_InmuebleIdInmueble",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Inquilino_InquilinoIdInquilino",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_InmuebleIdInmueble",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_InquilinoIdInquilino",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_CobroMensual_ContratoIdContrato",
                table: "CobroMensual");

            migrationBuilder.DropColumn(
                name: "InmuebleIdInmueble",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "InquilinoIdInquilino",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ContratoIdContrato",
                table: "CobroMensual");

            migrationBuilder.CreateIndex(
                name: "IX_Inmueble_IdEdificio",
                table: "Inmueble",
                column: "IdEdificio");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_IdInmueble",
                table: "Contrato",
                column: "IdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_IdInquilino",
                table: "Contrato",
                column: "IdInquilino");

            migrationBuilder.CreateIndex(
                name: "IX_CobroMensual_IdContrato",
                table: "CobroMensual",
                column: "IdContrato");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroMensual_Contrato_IdContrato",
                table: "CobroMensual",
                column: "IdContrato",
                principalTable: "Contrato",
                principalColumn: "IdContrato",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Inmueble_IdInmueble",
                table: "Contrato",
                column: "IdInmueble",
                principalTable: "Inmueble",
                principalColumn: "IdInmueble",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Inquilino_IdInquilino",
                table: "Contrato",
                column: "IdInquilino",
                principalTable: "Inquilino",
                principalColumn: "IdInquilino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inmueble_Edificio_IdEdificio",
                table: "Inmueble",
                column: "IdEdificio",
                principalTable: "Edificio",
                principalColumn: "IdEdificio",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobroMensual_Contrato_IdContrato",
                table: "CobroMensual");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Inmueble_IdInmueble",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Inquilino_IdInquilino",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmueble_Edificio_IdEdificio",
                table: "Inmueble");

            migrationBuilder.DropIndex(
                name: "IX_Inmueble_IdEdificio",
                table: "Inmueble");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_IdInmueble",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_IdInquilino",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_CobroMensual_IdContrato",
                table: "CobroMensual");

            migrationBuilder.AddColumn<int>(
                name: "InmuebleIdInmueble",
                table: "Contrato",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InquilinoIdInquilino",
                table: "Contrato",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContratoIdContrato",
                table: "CobroMensual",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InmuebleIdInmueble",
                table: "Contrato",
                column: "InmuebleIdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InquilinoIdInquilino",
                table: "Contrato",
                column: "InquilinoIdInquilino");

            migrationBuilder.CreateIndex(
                name: "IX_CobroMensual_ContratoIdContrato",
                table: "CobroMensual",
                column: "ContratoIdContrato");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroMensual_Contrato_ContratoIdContrato",
                table: "CobroMensual",
                column: "ContratoIdContrato",
                principalTable: "Contrato",
                principalColumn: "IdContrato");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Inmueble_InmuebleIdInmueble",
                table: "Contrato",
                column: "InmuebleIdInmueble",
                principalTable: "Inmueble",
                principalColumn: "IdInmueble");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Inquilino_InquilinoIdInquilino",
                table: "Contrato",
                column: "InquilinoIdInquilino",
                principalTable: "Inquilino",
                principalColumn: "IdInquilino");
        }
    }
}
