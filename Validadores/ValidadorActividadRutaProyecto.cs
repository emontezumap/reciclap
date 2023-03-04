
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorActividadRutaProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private ActividadRutaProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorActividadRutaProyecto(ActividadRutaProyectoDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdRutaProyecto", new HashSet<CodigosError>() },
			{ "Descripcion", new HashSet<CodigosError>() },
			{ "Secuencia", new HashSet<CodigosError>() },
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
            else if (await ctx.ActividadesRutasProyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdRutaProyecto == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdRutaProyecto != null && await ctx.Varias.FindAsync(dto.IdRutaProyecto) == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Descripcion != null && dto.Descripcion.Length > 450)
            mensajes["Descripcion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

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
