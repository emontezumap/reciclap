using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Valores_por_defecto_Activo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "usuarios",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "tipo_publicacion",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "roles",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "publicaciones",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "profesiones",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "personal",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "paises",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "es_administrador",
                table: "grupos",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "grupos",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "estatus_publicaciones",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "estados",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "comentarios",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "ciudades",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "chats",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "tipo_publicacion",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "roles",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "publicaciones",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "profesiones",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "personal",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "paises",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "es_administrador",
                table: "grupos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "grupos",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "estatus_publicaciones",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "estados",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "comentarios",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "ciudades",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "chats",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");
        }
    }
}
