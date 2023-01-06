using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Relacion_M_a_N : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_id_rol",
                table: "personal");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_id_rol",
                table: "personal");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol",
                unique: true);
        }
    }
}
