using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Claves_unicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_usuarios_nombre_segundo_nombre_apellido_segundo_apellido",
                table: "usuarios",
                columns: new[] { "nombre", "segundo_nombre", "apellido", "segundo_apellido" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tipo_publicacion_descripcion",
                table: "tipo_publicacion",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_descripcion",
                table: "roles",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_titulo",
                table: "publicaciones",
                column: "titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_descripcion",
                table: "profesiones",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paises_nombre",
                table: "paises",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grupos_descripcion",
                table: "grupos",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_descripcion",
                table: "estatus_publicaciones",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estados_nombre",
                table: "estados",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_nombre",
                table: "ciudades",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chats_titulo",
                table: "chats",
                column: "titulo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_nombre_segundo_nombre_apellido_segundo_apellido",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_tipo_publicacion_descripcion",
                table: "tipo_publicacion");

            migrationBuilder.DropIndex(
                name: "IX_roles_descripcion",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_publicaciones_titulo",
                table: "publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_profesiones_descripcion",
                table: "profesiones");

            migrationBuilder.DropIndex(
                name: "IX_paises_nombre",
                table: "paises");

            migrationBuilder.DropIndex(
                name: "IX_grupos_descripcion",
                table: "grupos");

            migrationBuilder.DropIndex(
                name: "IX_estatus_publicaciones_descripcion",
                table: "estatus_publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_estados_nombre",
                table: "estados");

            migrationBuilder.DropIndex(
                name: "IX_ciudades_nombre",
                table: "ciudades");

            migrationBuilder.DropIndex(
                name: "IX_chats_titulo",
                table: "chats");
        }
    }
}
