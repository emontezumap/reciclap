
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorRastreoPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private RastreoPublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorRastreoPublicacion(RastreoPublicacionDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdPublicacion", new HashSet<CodigosError>() },
			{ "IdFasePublicacion", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "IdUsuario", new HashSet<CodigosError>() },
			{ "TiempoEstimado", new HashSet<CodigosError>() },
			{ "IdFaseAnterior", new HashSet<CodigosError>() },
			{ "IdFaseSiguiente", new HashSet<CodigosError>() },
			{ "Comentarios", new HashSet<CodigosError>() },
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
            else if (await ctx.RastreoPublicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdPublicacion == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdPublicacion != null && await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdFasePublicacion == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdFasePublicacion != null && await ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdFaseAnterior != null && await ctx.Varias.FindAsync(dto.IdFaseAnterior) == null)
            mensajes["IdFaseAnterior"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdFaseSiguiente != null && await ctx.Varias.FindAsync(dto.IdFaseSiguiente) == null)
            mensajes["IdFaseSiguiente"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Comentarios))
        {
            if (op == Operacion.Creacion)
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Comentarios != null)   // Cadena vacia
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Comentarios != null && dto.Comentarios.Length > 100)
            mensajes["Comentarios"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

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
