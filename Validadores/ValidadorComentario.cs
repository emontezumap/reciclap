using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorComentario : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private ComentarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorComentario(ComentarioDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "IdChat", new List<string>()},
            { "IdUsuario", new List<string>() },
            { "Fecha", new List<string>()},
            { "Texto", new List<string>()},
            { "IdComentario", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion && await ctx.Comentarios.FindAsync(dto.Id) == null)
        {
            mensajes["Id"].Add("El comentario especificado no existe");
            hayError = true;
        }

        if (dto.IdChat == null)
        {
            mensajes["IdChat"].Add("Se requiere un chat");
            hayError = true;
        }
        else if (await ctx.Chats.FindAsync(dto.IdChat) == null)
        {
            mensajes["IdChat"].Add("El chat especificado no existe");
            hayError = true;
        }

        if (dto.IdUsuario == null)
        {
            mensajes["IdUsuario"].Add("Se requiere el usuario creador del comentario");
            hayError = true;
        }
        else if (await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
        {
            mensajes["IdUsuario"].Add("El usuario especificado no existe");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.Fecha == null)
        {
            mensajes["Fecha"].Add("Se requiere la fecha de creación del comentario");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Texto))
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el texto del comentario");
                hayError = true;
            }
            else if (dto.Texto != null)
            {
                mensajes["Nombre"].Add("Se requiere el texto del comentario");
                hayError = true;
            }

        if (dto.IdComentario != null && await ctx.Chats.FindAsync(dto.IdChat) == null)
        {
            mensajes["IdComentario"].Add("El comentario citado no existe");
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