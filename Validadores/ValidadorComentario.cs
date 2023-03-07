
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorComentario : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private ComentarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorComentario(ComentarioDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdChat", new HashSet<string>() },
			{ "IdUsuario", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
			{ "Texto", new HashSet<string>() },
			{ "IdCita", new HashSet<string>() },
			{ "Activo", new HashSet<string>() }
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
        this.ValidarReferencias = ValidarReferencias;
    }

    public async Task<ResultadoValidacion> Validar()
    {

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
                mensajes["Id"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (await ctx.Comentarios.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdChat == null)
            mensajes["IdChat"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdChat != null && await ctx.Chats.FindAsync(dto.IdChat) == null)
            mensajes["IdChat"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (string.IsNullOrEmpty(dto.Texto))
        {
            if (op == Operacion.Creacion)
                mensajes["Texto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Texto != null)   // Cadena vacia
                mensajes["Texto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (ValidarReferencias && dto.IdCita != null && await ctx.Comentarios.FindAsync(dto.IdCita) == null)
            mensajes["IdCita"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        bool hayError = false;

        foreach (KeyValuePair<string, HashSet<string>> entry in mensajes)
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
