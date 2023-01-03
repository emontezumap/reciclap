using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Relacion_usuario_creadormodificador1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_TipoPublicacion_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPublicacion",
                table: "TipoPublicacion");

            migrationBuilder.RenameTable(
                name: "TipoPublicacion",
                newName: "TiposPublicacion");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "usuarios",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "usuarios",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "usuarios",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "roles",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "roles",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creador",
                table: "roles",
                newName: "es_creador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "roles",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "publicaciones",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "publicaciones",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "publicaciones",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "profesiones",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "profesiones",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "profesiones",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "personal",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "personal",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "personal",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "paises",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "paises",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "paises",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "estatus_publicaciones",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "estatus_publicaciones",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "estatus_publicaciones",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "estados",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "estados",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "estados",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "comentarios",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "comentarios",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "comentarios",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "ciudades",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "ciudades",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "ciudades",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "chats",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "chats",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "chats",
                newName: "id_creador");

            migrationBuilder.RenameColumn(
                name: "ultima_modificacion",
                table: "TiposPublicacion",
                newName: "fecha_modificacion");

            migrationBuilder.RenameColumn(
                name: "modificado_por",
                table: "TiposPublicacion",
                newName: "id_modificador");

            migrationBuilder.RenameColumn(
                name: "creado_por",
                table: "TiposPublicacion",
                newName: "id_creador");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "TiposPublicacion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposPublicacion",
                table: "TiposPublicacion",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_creador",
                table: "chats",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_modificador",
                table: "chats",
                column: "id_modificador");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_usuarios_id_creador",
                table: "chats",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_usuarios_id_modificador",
                table: "chats",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_TiposPublicacion_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion",
                principalTable: "TiposPublicacion",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_usuarios_id_creador",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "FK_chats_usuarios_id_modificador",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_TiposPublicacion_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_chats_id_creador",
                table: "chats");

            migrationBuilder.DropIndex(
                name: "IX_chats_id_modificador",
                table: "chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposPublicacion",
                table: "TiposPublicacion");

            migrationBuilder.RenameTable(
                name: "TiposPublicacion",
                newName: "TipoPublicacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "usuarios",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "usuarios",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "usuarios",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "roles",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "roles",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "roles",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "es_creador",
                table: "roles",
                newName: "creador");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "publicaciones",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "publicaciones",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "publicaciones",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "profesiones",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "profesiones",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "profesiones",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "personal",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "personal",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "personal",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "paises",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "paises",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "paises",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "estatus_publicaciones",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "estatus_publicaciones",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "estatus_publicaciones",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "estados",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "estados",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "estados",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "comentarios",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "comentarios",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "comentarios",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "ciudades",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "ciudades",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "ciudades",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "chats",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "chats",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "chats",
                newName: "ultima_modificacion");

            migrationBuilder.RenameColumn(
                name: "id_modificador",
                table: "TipoPublicacion",
                newName: "modificado_por");

            migrationBuilder.RenameColumn(
                name: "id_creador",
                table: "TipoPublicacion",
                newName: "creado_por");

            migrationBuilder.RenameColumn(
                name: "fecha_modificacion",
                table: "TipoPublicacion",
                newName: "ultima_modificacion");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "TipoPublicacion",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "newid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPublicacion",
                table: "TipoPublicacion",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_TipoPublicacion_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion",
                principalTable: "TipoPublicacion",
                principalColumn: "id");
        }
    }
}
