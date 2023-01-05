using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Valor_por_defecto8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "tipo_publicacion",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "tipo_publicacion",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "roles",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "roles",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "paises",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "paises",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "grupos",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "grupos",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "estados",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "estados",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "null",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "tipo_publicacion",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "tipo_publicacion",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "roles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "roles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "profesiones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "personal",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "paises",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "paises",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "grupos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "grupos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "estatus_publicaciones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_modificador",
                table: "estados",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_creador",
                table: "estados",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");
        }
    }
}
