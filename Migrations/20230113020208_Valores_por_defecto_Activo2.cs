using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Valores_por_defecto_Activo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "usuarios",
                type: "bit",
                nullable: true,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "ciudades",
                type: "bit",
                nullable: true,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "usuarios",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "1");

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "ciudades",
                type: "bit",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "1");
        }
    }
}
