using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Recreacion_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id_grupo",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_grupo",
                table: "usuarios",
                column: "id_grupo");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_grupos_id_grupo",
                table: "usuarios",
                column: "id_grupo",
                principalTable: "grupos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_grupos_id_grupo",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_grupo",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "id_grupo",
                table: "usuarios");
        }
    }
}
