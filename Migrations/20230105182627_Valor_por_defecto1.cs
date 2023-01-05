using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Valor_por_defecto1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_TiposPublicacion_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_creador",
                table: "TiposPublicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposPublicacion_usuarios_id_modificador",
                table: "TiposPublicacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposPublicacion",
                table: "TiposPublicacion");

            migrationBuilder.RenameTable(
                name: "TiposPublicacion",
                newName: "tipo_publicacion");

            migrationBuilder.RenameIndex(
                name: "IX_TiposPublicacion_id_modificador",
                table: "tipo_publicacion",
                newName: "IX_tipo_publicacion_id_modificador");

            migrationBuilder.RenameIndex(
                name: "IX_TiposPublicacion_id_creador",
                table: "tipo_publicacion",
                newName: "IX_tipo_publicacion_id_creador");

            migrationBuilder.AlterColumn<string>(
                name: "segundo_nombre",
                table: "usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "''",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo_publicacion",
                table: "tipo_publicacion",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_tipo_publicacion_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion",
                principalTable: "tipo_publicacion",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tipo_publicacion_usuarios_id_creador",
                table: "tipo_publicacion",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tipo_publicacion_usuarios_id_modificador",
                table: "tipo_publicacion",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_tipo_publicacion_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_tipo_publicacion_usuarios_id_creador",
                table: "tipo_publicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_tipo_publicacion_usuarios_id_modificador",
                table: "tipo_publicacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo_publicacion",
                table: "tipo_publicacion");

            migrationBuilder.RenameTable(
                name: "tipo_publicacion",
                newName: "TiposPublicacion");

            migrationBuilder.RenameIndex(
                name: "IX_tipo_publicacion_id_modificador",
                table: "TiposPublicacion",
                newName: "IX_TiposPublicacion_id_modificador");

            migrationBuilder.RenameIndex(
                name: "IX_tipo_publicacion_id_creador",
                table: "TiposPublicacion",
                newName: "IX_TiposPublicacion_id_creador");

            migrationBuilder.AlterColumn<string>(
                name: "segundo_nombre",
                table: "usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "''");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposPublicacion",
                table: "TiposPublicacion",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_TiposPublicacion_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion",
                principalTable: "TiposPublicacion",
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
    }
}
