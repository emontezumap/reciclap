using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Relacion_usuario_creadormodificador4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_creador",
                table: "usuarios",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_modificador",
                table: "usuarios",
                column: "id_modificador");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_usuarios_id_creador",
                table: "usuarios",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_usuarios_id_modificador",
                table: "usuarios",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_usuarios_id_creador",
                table: "usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_usuarios_id_modificador",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_creador",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_modificador",
                table: "usuarios");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
