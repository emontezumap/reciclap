using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Clave_unica_Personal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_personal_id_publicacion_id_rol",
                table: "personal",
                columns: new[] { "id_publicacion", "id_rol" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_id_publicacion_id_rol",
                table: "personal");
        }
    }
}
