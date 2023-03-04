
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorComentario : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private ComentarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorComentario(ComentarioDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdChat", new HashSet<CodigosError>() },
			{ "IdUsuario", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "Texto", new HashSet<CodigosError>() },
			{ "IdCita", new HashSet<CodigosError>() },
			{ "Activo", new HashSet<CodigosError>() }
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
        this.SinReferencias = SinReferencias;
    }

    public async Task<ResultadoValidacion> Validar()
    {

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
                mensajes["Id"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Comentarios.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdChat == null)
            mensajes["IdChat"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdChat != null && await ctx.Chats.FindAsync(dto.IdChat) == null)
            mensajes["IdChat"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Texto))
        {
            if (op == Operacion.Creacion)
                mensajes["Texto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Texto != null)   // Cadena vacia
                mensajes["Texto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (!SinReferencias && dto.IdCita != null && await ctx.Comentarios.FindAsync(dto.IdCita) == null)
            mensajes["IdCita"].Add(CodigosError.ERR_ID_INEXISTENTE);

        bool hayError = false;

        foreach (KeyValuePair<string, HashSet<CodigosError>> entry in mensajes)
        {
            if (entry.Value.Count > 0)
            {
                hayError = true;
                break;
            }
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
