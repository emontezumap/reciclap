using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Valor_por_defecto_para_profesion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id_profesion",
                table: "usuarios",
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
                name: "id_profesion",
                table: "usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "null");
        }
    }
}
