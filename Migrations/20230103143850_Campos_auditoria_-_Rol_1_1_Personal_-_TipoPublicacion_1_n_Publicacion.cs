using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Campos_auditoria__Rol_1_1_Personal__TipoPublicacion_1_n_Publicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_id_rol",
                table: "personal");

            migrationBuilder.AlterColumn<string>(
                name: "telefono2",
                table: "usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_profesion",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "publicaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "publicaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "id_tipo_publicacion",
                table: "publicaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "publicaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "publicaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "profesiones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "profesiones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "personal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "personal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "paises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "paises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "paises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "paises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "estatus_publicaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "estatus_publicaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "estados",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "estados",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "comentarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "comentarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "comentarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "comentarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "ciudades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "ciudades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "ciudades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "ciudades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creado_por",
                table: "chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "modificado_por",
                table: "chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ultima_modificacion",
                table: "chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TipoPublicacion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    creado_por = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_por = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ultima_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPublicacion", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_publicaciones_TipoPublicacion_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion",
                principalTable: "TipoPublicacion",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicaciones_TipoPublicacion_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropTable(
                name: "TipoPublicacion");

            migrationBuilder.DropIndex(
                name: "IX_publicaciones_id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_personal_id_rol",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "id_tipo_publicacion",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "profesiones");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "profesiones");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "profesiones");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "profesiones");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "paises");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "paises");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "paises");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "paises");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "estatus_publicaciones");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "estatus_publicaciones");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "estatus_publicaciones");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "estatus_publicaciones");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "ciudades");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "ciudades");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "ciudades");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "ciudades");

            migrationBuilder.DropColumn(
                name: "creado_por",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "modificado_por",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "ultima_modificacion",
                table: "chats");

            migrationBuilder.AlterColumn<string>(
                name: "telefono2",
                table: "usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_profesion",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol");
        }
    }
}
