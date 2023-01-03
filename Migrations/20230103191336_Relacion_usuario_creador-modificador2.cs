using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Relacion_usuario_creadormodificador2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TiposPublicacion_id_creador",
                table: "TiposPublicacion",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_TiposPublicacion_id_modificador",
                table: "TiposPublicacion",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_roles_id_creador",
                table: "roles",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_roles_id_modificador",
                table: "roles",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_creador",
                table: "publicaciones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_modificador",
                table: "publicaciones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_id_creador",
                table: "profesiones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_id_modificador",
                table: "profesiones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_paises_id_creador",
                table: "paises",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_paises_id_modificador",
                table: "paises",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_id_creador",
                table: "estatus_publicaciones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_id_modificador",
                table: "estatus_publicaciones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_creador",
                table: "estados",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_modificador",
                table: "estados",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_creador",
                table: "comentarios",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_modificador",
                table: "comentarios",
                column: "id_modificador");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_id_creador",
                table: "comentarios",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_id_modificador",
                table: "comentarios",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estados_usuarios_id_creador",
                table: "estados",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estados_usuarios_id_modificador",
                table: "estados",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_creador",
                table: "estatus_publicaciones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_modificador",
                table: "estatus_publicaciones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_paises_usuarios_id_creador",
                table: "paises",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_paises_usuarios_id_modificador",
                table: "paises",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_profesiones_usuarios_id_creador",
                table: "profesiones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_profesiones_usuarios_id_modificador",
                table: "profesiones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_usuarios_id_creador",
                table: "publicaciones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_usuarios_id_modificador",
                table: "publicaciones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_usuarios_id_creador",
                table: "roles",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_usuarios_id_modificador",
                table: "roles",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_creador",
                table: "TiposPublicacion",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_modificador",
                table: "TiposPublicacion",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_usuarios_id_creador",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_usuarios_id_modificador",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_estados_usuarios_id_creador",
                table: "estados");

            migrationBuilder.DropForeignKey(
                name: "FK_estados_usuarios_id_modificador",
                table: "estados");

            migrationBuilder.DropForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_creador",
                table: "estatus_publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_modificador",
                table: "estatus_publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_paises_usuarios_id_creador",
                table: "paises");

            migrationBuilder.DropForeignKey(
                name: "FK_paises_usuarios_id_modificador",
                table: "paises");

            migrationBuilder.DropForeignKey(
                name: "FK_profesiones_usuarios_id_creador",
                table: "profesiones");

            migrationBuilder.DropForeignKey(
                name: "FK_profesiones_usuarios_id_modificador",
                table: "profesiones");

            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_usuarios_id_creador",
                table: "publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_usuarios_id_modificador",
                table: "publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_usuarios_id_creador",
                table: "roles");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_usuarios_id_modificador",
                table: "roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_creador",
                table: "TiposPublicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_modificador",
                table: "TiposPublicacion");

            migrationBuilder.DropIndex(
                name: "IX_TiposPublicacion_id_creador",
                table: "TiposPublicacion");

            migrationBuilder.DropIndex(
                name: "IX_TiposPublicacion_id_modificador",
                table: "TiposPublicacion");

            migrationBuilder.DropIndex(
                name: "IX_roles_id_creador",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_id_modificador",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_publicaciones_id_creador",
                table: "publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_publicaciones_id_modificador",
                table: "publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_profesiones_id_creador",
                table: "profesiones");

            migrationBuilder.DropIndex(
                name: "IX_profesiones_id_modificador",
                table: "profesiones");

            migrationBuilder.DropIndex(
                name: "IX_paises_id_creador",
                table: "paises");

            migrationBuilder.DropIndex(
                name: "IX_paises_id_modificador",
                table: "paises");

            migrationBuilder.DropIndex(
                name: "IX_estatus_publicaciones_id_creador",
                table: "estatus_publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_estatus_publicaciones_id_modificador",
                table: "estatus_publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_estados_id_creador",
                table: "estados");

            migrationBuilder.DropIndex(
                name: "IX_estados_id_modificador",
                table: "estados");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_id_creador",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_id_modificador",
                table: "comentarios");
        }
    }
}
