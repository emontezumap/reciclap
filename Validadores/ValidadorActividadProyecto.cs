
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorActividadProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private ActividadProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorActividadProyecto(ActividadProyectoDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdProyecto", new HashSet<CodigosError>() },
			{ "IdRutaProyecto", new HashSet<CodigosError>() },
			{ "Secuencia", new HashSet<CodigosError>() },
			{ "IdActividadRuta", new HashSet<CodigosError>() },
			{ "Descripcion", new HashSet<CodigosError>() },
			{ "FechaInicio", new HashSet<CodigosError>() },
			{ "FechaFinalizacion", new HashSet<CodigosError>() },
			{ "IdEjecutor", new HashSet<CodigosError>() },
			{ "IdRevisor", new HashSet<CodigosError>() },
			{ "IdEstatusPublicacion", new HashSet<CodigosError>() },
			{ "IdEstatusProyecto", new HashSet<CodigosError>() },
			{ "IdRevisadaPor", new HashSet<CodigosError>() },
			{ "IdTipoActividad", new HashSet<CodigosError>() },
			{ "TiempoEstimado", new HashSet<CodigosError>() },
			{ "ProgresoEstimado", new HashSet<CodigosError>() },
			{ "Evaluacion", new HashSet<CodigosError>() },
			{ "FechaDisponible", new HashSet<CodigosError>() },
			{ "TotalArticulos", new HashSet<CodigosError>() },
			{ "CostoEstimado", new HashSet<CodigosError>() },
			{ "IdMonedaCostoEstimado", new HashSet<CodigosError>() },
			{ "TipoCambioCostoEstimado", new HashSet<CodigosError>() },
			{ "CostoReal", new HashSet<CodigosError>() },
			{ "IdMonedaCostoReal", new HashSet<CodigosError>() },
			{ "TipoCambioCostoReal", new HashSet<CodigosError>() },
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
            else if (await ctx.ActividadesProyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdProyecto == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdRutaProyecto == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdRutaProyecto != null && await ctx.Varias.FindAsync(dto.IdRutaProyecto) == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion && dto.IdActividadRuta == null)
            mensajes["IdActividadRuta"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdActividadRuta != null && await ctx.ActividadesRutasProyectos.FindAsync(dto.IdActividadRuta) == null)
            mensajes["IdActividadRuta"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (op == Operacion.Creacion && dto.FechaInicio == null)
            mensajes["FechaInicio"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdEjecutor != null && await ctx.Usuarios.FindAsync(dto.IdEjecutor) == null)
            mensajes["IdEjecutor"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdRevisor != null && await ctx.Usuarios.FindAsync(dto.IdRevisor) == null)
            mensajes["IdRevisor"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdEstatusPublicacion == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdEstatusPublicacion != null && await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdEstatusProyecto == null)
            mensajes["IdEstatusProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdEstatusProyecto != null && await ctx.Varias.FindAsync(dto.IdEstatusProyecto) == null)
            mensajes["IdEstatusProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdTipoActividad == null)
            mensajes["IdTipoActividad"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoActividad != null && await ctx.Varias.FindAsync(dto.IdTipoActividad) == null)
            mensajes["IdTipoActividad"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.IdMonedaCostoEstimado))
        {
            if (op == Operacion.Creacion)
                mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.IdMonedaCostoEstimado != null)   // Cadena vacia
                mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.IdMonedaCostoEstimado != null && dto.IdMonedaCostoEstimado.Length > 3)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (op == Operacion.Creacion && dto.IdMonedaCostoEstimado == null)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdMonedaCostoEstimado != null && await ctx.Monedas.FindAsync(dto.IdMonedaCostoEstimado) == null)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.IdMonedaCostoReal))
        {
            if (op == Operacion.Creacion)
                mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.IdMonedaCostoReal != null)   // Cadena vacia
                mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.IdMonedaCostoReal != null && dto.IdMonedaCostoReal.Length > 3)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (op == Operacion.Creacion && dto.IdMonedaCostoReal == null)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdMonedaCostoReal != null && await ctx.Monedas.FindAsync(dto.IdMonedaCostoReal) == null)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
