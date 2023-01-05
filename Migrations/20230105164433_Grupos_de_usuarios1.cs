using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Grupos_de_usuarios1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "TiposPublicacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "publicaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "profesiones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "personal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "paises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "estatus_publicaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "estados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "comentarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "ciudades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "grupos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    es_administrador = table.Column<bool>(type: "bit", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos", x => x.id);
                    table.ForeignKey(
                        name: "FK_grupos_usuarios_id_creador",
                        column: x => x.id_creador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_grupos_usuarios_id_modificador",
                        column: x => x.id_modificador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_grupos_id_creador",
                table: "grupos",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_grupos_id_modificador",
                table: "grupos",
                column: "id_modificador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grupos");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "TiposPublicacion");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "publicaciones");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "profesiones");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "paises");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "estatus_publicaciones");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "ciudades");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "chats");
        }
    }
}
