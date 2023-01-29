using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wapi.Migrations
{
    public partial class Administradores_Articulo_DetallePublicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administradores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    telefono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, defaultValueSql: "''"),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administradores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articulos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "''"),
                    ruta_foto = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "''"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1"),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    id_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, defaultValueSql: "''"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    id_estado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    id_chat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "''"),
                    id_comentario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
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
                });

            migrationBuilder.CreateTable(
                name: "detalle_publicaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    id_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_articulo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "''"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_publicaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_publicaciones_articulos_id_articulo",
                        column: x => x.id_articulo,
                        principalTable: "articulos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    id_pais = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estatus_publicaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "''"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estatus_publicaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grupos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    es_administrador = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paises",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personal",
                columns: table => new
                {
                    id_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_rol = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal", x => new { x.id_publicacion, x.id_usuario });
                });

            migrationBuilder.CreateTable(
                name: "profesiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesiones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    segundo_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    segundo_apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    perfil = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "''"),
                    direccion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, defaultValueSql: "''"),
                    id_ciudad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "''"),
                    telefono2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "''"),
                    email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''"),
                    clave = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    email2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''"),
                    id_profesion = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    max_publicaciones = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    id_grupo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
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
                        name: "FK_usuarios_grupos_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "grupos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuarios_profesiones_id_profesion",
                        column: x => x.id_profesion,
                        principalTable: "profesiones",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuarios_usuarios_id_creador",
                        column: x => x.id_creador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuarios_usuarios_id_modificador",
                        column: x => x.id_modificador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "''"),
                    es_creador = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_usuarios_id_creador",
                        column: x => x.id_creador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_roles_usuarios_id_modificador",
                        column: x => x.id_modificador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tipos_publicacion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "''"),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "null"),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_publicacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_tipos_publicacion_usuarios_id_creador",
                        column: x => x.id_creador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tipos_publicacion_usuarios_id_modificador",
                        column: x => x.id_modificador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "publicaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "''"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "''"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    gustan = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    no_gustan = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    id_estatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_tipo_publicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_creador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    id_modificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_publicaciones_estatus_publicaciones_id_estatus",
                        column: x => x.id_estatus,
                        principalTable: "estatus_publicaciones",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_publicaciones_tipos_publicacion_id_tipo_publicacion",
                        column: x => x.id_tipo_publicacion,
                        principalTable: "tipos_publicacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_publicaciones_usuarios_id_creador",
                        column: x => x.id_creador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_publicaciones_usuarios_id_modificador",
                        column: x => x.id_modificador,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_administradores_email",
                table: "administradores",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_administradores_nombre",
                table: "administradores",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_articulos_CreadorId",
                table: "articulos",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_descripcion",
                table: "articulos",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_articulos_ModificadorId",
                table: "articulos",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_creador",
                table: "chats",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_modificador",
                table: "chats",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_chats_id_publicacion",
                table: "chats",
                column: "id_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_chats_titulo",
                table: "chats",
                column: "titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_creador",
                table: "ciudades",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_estado",
                table: "ciudades",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_id_modificador",
                table: "ciudades",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_nombre_id_estado",
                table: "ciudades",
                columns: new[] { "nombre", "id_estado" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_chat",
                table: "comentarios",
                column: "id_chat");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_comentario",
                table: "comentarios",
                column: "id_comentario");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_creador",
                table: "comentarios",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_modificador",
                table: "comentarios",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_usuario",
                table: "comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_publicaciones_id_articulo_id_publicacion",
                table: "detalle_publicaciones",
                columns: new[] { "id_articulo", "id_publicacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_publicaciones_id_creador",
                table: "detalle_publicaciones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_publicaciones_id_modificador",
                table: "detalle_publicaciones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_publicaciones_id_publicacion",
                table: "detalle_publicaciones",
                column: "id_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_creador",
                table: "estados",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_modificador",
                table: "estados",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_estados_id_pais",
                table: "estados",
                column: "id_pais");

            migrationBuilder.CreateIndex(
                name: "IX_estados_nombre_id_pais",
                table: "estados",
                columns: new[] { "nombre", "id_pais" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_descripcion",
                table: "estatus_publicaciones",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_id_creador",
                table: "estatus_publicaciones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_estatus_publicaciones_id_modificador",
                table: "estatus_publicaciones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_grupos_descripcion",
                table: "grupos",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grupos_id_creador",
                table: "grupos",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_grupos_id_modificador",
                table: "grupos",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_paises_id_creador",
                table: "paises",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_paises_id_modificador",
                table: "paises",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_paises_nombre",
                table: "paises",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_creador",
                table: "personal",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_modificador",
                table: "personal",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_publicacion_id_rol",
                table: "personal",
                columns: new[] { "id_publicacion", "id_rol" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_rol",
                table: "personal",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_usuario",
                table: "personal",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_descripcion",
                table: "profesiones",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_id_creador",
                table: "profesiones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_profesiones_id_modificador",
                table: "profesiones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_creador",
                table: "publicaciones",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_estatus",
                table: "publicaciones",
                column: "id_estatus");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_modificador",
                table: "publicaciones",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_id_tipo_publicacion",
                table: "publicaciones",
                column: "id_tipo_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_titulo",
                table: "publicaciones",
                column: "titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_descripcion",
                table: "roles",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_id_creador",
                table: "roles",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_roles_id_modificador",
                table: "roles",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_tipos_publicacion_descripcion",
                table: "tipos_publicacion",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tipos_publicacion_id_creador",
                table: "tipos_publicacion",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_tipos_publicacion_id_modificador",
                table: "tipos_publicacion",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_ciudad",
                table: "usuarios",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_creador",
                table: "usuarios",
                column: "id_creador");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_grupo",
                table: "usuarios",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_modificador",
                table: "usuarios",
                column: "id_modificador");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_profesion",
                table: "usuarios",
                column: "id_profesion");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_nombre_segundo_nombre_apellido_segundo_apellido",
                table: "usuarios",
                columns: new[] { "nombre", "segundo_nombre", "apellido", "segundo_apellido" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_usuarios_CreadorId",
                table: "articulos",
                column: "CreadorId",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_usuarios_ModificadorId",
                table: "articulos",
                column: "ModificadorId",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_publicaciones_id_publicacion",
                table: "chats",
                column: "id_publicacion",
                principalTable: "publicaciones",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_usuarios_id_creador",
                table: "chats",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_usuarios_id_modificador",
                table: "chats",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ciudades_estados_id_estado",
                table: "ciudades",
                column: "id_estado",
                principalTable: "estados",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ciudades_usuarios_id_creador",
                table: "ciudades",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ciudades_usuarios_id_modificador",
                table: "ciudades",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_id_creador",
                table: "comentarios",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_id_modificador",
                table: "comentarios",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_id_usuario",
                table: "comentarios",
                column: "id_usuario",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_publicaciones_publicaciones_id_publicacion",
                table: "detalle_publicaciones",
                column: "id_publicacion",
                principalTable: "publicaciones",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_publicaciones_usuarios_id_creador",
                table: "detalle_publicaciones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_publicaciones_usuarios_id_modificador",
                table: "detalle_publicaciones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estados_paises_id_pais",
                table: "estados",
                column: "id_pais",
                principalTable: "paises",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estados_usuarios_id_creador",
                table: "estados",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estados_usuarios_id_modificador",
                table: "estados",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_creador",
                table: "estatus_publicaciones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_estatus_publicaciones_usuarios_id_modificador",
                table: "estatus_publicaciones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_grupos_usuarios_id_creador",
                table: "grupos",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_grupos_usuarios_id_modificador",
                table: "grupos",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_paises_usuarios_id_creador",
                table: "paises",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_paises_usuarios_id_modificador",
                table: "paises",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_publicaciones_id_publicacion",
                table: "personal",
                column: "id_publicacion",
                principalTable: "publicaciones",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_roles_id_rol",
                table: "personal",
                column: "id_rol",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_usuarios_id_creador",
                table: "personal",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_usuarios_id_modificador",
                table: "personal",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_usuarios_id_usuario",
                table: "personal",
                column: "id_usuario",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_profesiones_usuarios_id_creador",
                table: "profesiones",
                column: "id_creador",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_profesiones_usuarios_id_modificador",
                table: "profesiones",
                column: "id_modificador",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ciudades_usuarios_id_creador",
                table: "ciudades");

            migrationBuilder.DropForeignKey(
                name: "FK_ciudades_usuarios_id_modificador",
                table: "ciudades");

            migrationBuilder.DropForeignKey(
                name: "FK_estados_usuarios_id_creador",
                table: "estados");

            migrationBuilder.DropForeignKey(
                name: "FK_estados_usuarios_id_modificador",
                table: "estados");

            migrationBuilder.DropForeignKey(
                name: "FK_grupos_usuarios_id_creador",
                table: "grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_grupos_usuarios_id_modificador",
                table: "grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_paises_usuarios_id_creador",
                table: "paises");

            migrationBuilder.DropForeignKey(
                name: "FK_paises_usuarios_id_modificador",
                table: "paises");

            migrationBuilder.DropForeignKey(
                name: "FK_profesiones_usuarios_id_creador",
                table: "profesiones");

            migrationBuilder.DropForeignKey(
                name: "FK_profesiones_usuarios_id_modificador",
                table: "profesiones");

            migrationBuilder.DropTable(
                name: "administradores");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "detalle_publicaciones");

            migrationBuilder.DropTable(
                name: "personal");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "articulos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "publicaciones");

            migrationBuilder.DropTable(
                name: "estatus_publicaciones");

            migrationBuilder.DropTable(
                name: "tipos_publicacion");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "ciudades");

            migrationBuilder.DropTable(
                name: "grupos");

            migrationBuilder.DropTable(
                name: "profesiones");

            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "paises");
        }
    }
}
