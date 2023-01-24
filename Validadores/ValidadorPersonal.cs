using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPersonal : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private PersonalDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorPersonal(PersonalDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "IdPublicacion", new List<string>() },
            { "IdUsuario", new List<string>() },
            { "Fecha", new List<string>()},
            { "IdRol", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (dto.IdPublicacion == null)
        {
            mensajes["IdPublicacion"].Add("Se debe especificar una publicación");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
        {
            mensajes["IdPublicacion"].Add("La publicación especificada no existe");
            hayError = true;
        }

        if (dto.IdUsuario == null)
        {
            mensajes["IdUsuario"].Add("Se requiere el usuario asignado a la publicación");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
        {
            mensajes["IdUsuario"].Add("El usuario especificado no existe");
            hayError = true;
        }

        if (op == Operacion.Modificacion && dto.IdPublicacion != null && dto.IdUsuario != null && await ctx.Personal.FindAsync(dto.IdPublicacion, dto.IdUsuario) == null)
        {
            mensajes["IdPublicacion"].Add("La combinación Publicacion-Usuario especificada no existe");
            mensajes["IdUsuario"].Add("La combinación Publicacion-Usuario especificada no existe");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.Fecha == null)
        {
            mensajes["Fecha"].Add("Se requiere la fecha de asignación del personal");
            hayError = true;
        }

        if (op == Operacion.Creacion)
        {
            if (dto.IdRol == null)
            {
                mensajes["IdRol"].Add("Se debe indicar el rol del personal");
                hayError = true;
            }
            else if (await ctx.Roles.FindAsync(dto.IdRol) == null)
            {
                mensajes["IdRol"].Add("El rol especificado no existe");
                hayError = true;
            }
        }
        else if (dto.IdRol != null && await ctx.Roles.FindAsync(dto.IdRol) == null)
        {
            mensajes["IdRol"].Add("El rol especificado no existe");
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