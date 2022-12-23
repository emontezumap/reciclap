using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class FluentAPI1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estatus_publicaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estatus_publicaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paises",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profesiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesiones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creador = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publicaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gustan = table.Column<int>(type: "int", nullable: false),
                    no_gustan = table.Column<int>(type: "int", nullable: false),
                    id_estatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_publicaciones_estatus_publicaciones_id_estatus",
                        column: x => x.id_estatus,
                        principalTable: "estatus_publicaciones",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_pais = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                    table.ForeignKey(
                        name: "FK_estados_paises_id_pais",
                        column: x => x.id_pais,
                        principalTable: "paises",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    id_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                    table.ForeignKey(
                        name: "FK_chats_publicaciones_id_publicacion",
                        column: x => x.id_publicacion,
                        principalTable: "publicaciones",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_estado = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudades", x => x.id);
                    table.ForeignKey(
                        name: "FK_ciudades_estados_id_estado",
                        column: x => x.id_estado,
                        principalTable: "estados",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    segundo_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    segundo_apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_ciudad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_profesion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    max_publicaciones = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_ciudades_id_ciudad",
                        column: x => x.id_ciudad,
                        principalTable: "ciudades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuarios_profesiones_id_profesion",
                        column: x => x.id_profesion,
                        principalTable: "profesiones",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    id_chat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_comentario = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_chats_id_chat",
                        column: x => x.id_chat,
                        principalTable: "chats",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comentarios_comentarios_id_comentario",
                        column: x => x.id_comentario,
                        principalTable: "comentarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personal",
                columns: table => new
                {
                    id_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_rol = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal", x => new { x.id_publicacion, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_personal_publicaciones_id_publicacion",
                        column: x => x.id_publicacion,
                        principalTable: "publicaciones",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_personal_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_personal_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_publicacion",
                table: "chats",
                column: "id_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_estado",
                table: "ciudades",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_chat",
                table: "comentarios",
                column: "id_chat");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_comentario",
                table: "comentarios",
                column: "id_comentario");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_usuario",
                table: "comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_pais",
                table: "estados",
                column: "id_pais");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_usuario",
                table: "personal",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_estatus",
                table: "publicaciones",
                column: "id_estatus");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_ciudad",
                table: "usuarios",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_profesion",
                table: "usuarios",
                column: "id_profesion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "personal");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "publicaciones");

            migrationBuilder.DropTable(
                name: "ciudades");

            migrationBuilder.DropTable(
                name: "profesiones");

            migrationBuilder.DropTable(
                name: "estatus_publicaciones");

            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "paises");
        }
    }
}
