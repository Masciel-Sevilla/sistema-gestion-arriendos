using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArriendosApi.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsActivo",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "EsPagado",
                table: "CobroMensual");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Inquilino",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Inmueble",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Edificio",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Contrato",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CobroMensual",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Inquilino");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Edificio");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CobroMensual");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "Inmueble",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "EsActivo",
                table: "Contrato",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsPagado",
                table: "CobroMensual",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
