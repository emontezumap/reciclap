
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorBitacoraProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private BitacoraProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorBitacoraProyecto(BitacoraProyectoDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdProyecto", new HashSet<CodigosError>() },
			{ "IdActividadProyecto", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "IdUsuario", new HashSet<CodigosError>() },
			{ "IdTipoBitacora", new HashSet<CodigosError>() },
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
            else if (await ctx.BitacorasProyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdProyecto == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdActividadProyecto == null)
            mensajes["IdActividadProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdActividadProyecto != null && await ctx.ActividadesProyectos.FindAsync(dto.IdActividadProyecto) == null)
            mensajes["IdActividadProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdTipoBitacora == null)
            mensajes["IdTipoBitacora"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoBitacora != null && await ctx.Varias.FindAsync(dto.IdTipoBitacora) == null)
            mensajes["IdTipoBitacora"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Comentarios))
        {
            if (op == Operacion.Creacion)
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Comentarios != null)   // Cadena vacia
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

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