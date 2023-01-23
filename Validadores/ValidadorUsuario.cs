using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorUsuario : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private UsuarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorUsuario(UsuarioDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Nombre", new List<string>()},
            { "Nombre2", new List<string>()},
            { "Apellido", new List<string>()},
            { "Apellido2", new List<string>()},
            { "Perfil", new List<string>()},
            { "Direccion", new List<string>()},
            { "IdCiudad", new List<string>()},
            { "Telefono", new List<string>() },
            { "Telefono2", new List<string>()},
            { "Email", new List<string>()},
            { "Clave", new List<string>()},
            { "Email2", new List<string>()},
            { "IdProfesion", new List<string>()},
            { "MaximoPublicaciones", new List<string>()},
            { "IdGrupo", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion && await ctx.Usuarios.FindAsync(dto.Id) == null)
        {
            mensajes["Id"].Add("El usuario especificado no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Nombre))
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del usuario");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del usuario");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Nombre) && dto.Nombre.Length > 50)
        {
            mensajes["Nombre"].Add("El nombre del usuario no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Descripcion))
            if (op == Operacion.Creacion)
            {
                mensajes["Descripcion"].Add("Se requiere una descripción");
                hayError = true;
            }
            else if (dto.Descripcion != null)
            {
                mensajes["Descripcion"].Add("Se requiere una descripción");
                hayError = true;
            }

        if (op == Operacion.Creacion && dto.Fecha == null)
        {
            mensajes["Fecha"].Add("Se requiere la fecha de asignación del personal");
            hayError = true;
        }

        if (dto.IdEstatus == null)
        {
            mensajes["IdEstatus"].Add("Se requiere el estatus de la publicación");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.EstatusPublicaciones.FindAsync(dto.IdEstatus) == null)
        {
            mensajes["IdEstatus"].Add("El estatus especificado no existe");
            hayError = true;
        }

        if (dto.IdTipoPublicacion == null)
        {
            mensajes["IdTipoPublicacion"].Add("Se requiere el tipo de publicación");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.TiposPublicacion.FindAsync(dto.IdTipoPublicacion) == null)
        {
            mensajes["IdTipoPublicacion"].Add("El tipo de publicación especificado no existe");
            hayError = true;
        }

        if ((op == Operacion.Creacion && dto.Activo == null))
        {
            mensajes["Activo"].Add("Se requiere un valor para el campo Activo");
            hayError = true;
        }

        ResultadoValidacion v = new ResultadoValidacion();
        v.ValidacionOk = !hayError;

        if (hayError)
        {
            v.Mensajes = mensajes;
            Ok = false;
        }
        else
        {
            Ok = true;
        }

        return v;
    }
}