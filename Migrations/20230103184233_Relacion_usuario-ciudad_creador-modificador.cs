using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Relacion_usuariociudad_creadormodificador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_creador",
                table: "ciudades",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_modificador",
                table: "ciudades",
                column: "id_modificador");

            migrationBuilder.AddForeignKey(
                name: "FK_ciudades_usuarios_id_creador",
                table: "ciudades",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ciudades_usuarios_id_modificador",
                table: "ciudades",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ciudades_usuarios_id_creador",
                table: "ciudades");

            migrationBuilder.DropForeignKey(
                name: "FK_ciudades_usuarios_id_modificador",
                table: "ciudades");

            migrationBuilder.DropIndex(
                name: "IX_ciudades_id_creador",
                table: "ciudades");

            migrationBuilder.DropIndex(
                name: "IX_ciudades_id_modificador",
                table: "ciudades");
        }
    }
}
