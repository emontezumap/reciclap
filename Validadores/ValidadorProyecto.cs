
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private ProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorProyecto(ProyectoDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "Titulo", new HashSet<CodigosError>() },
			{ "Descripcion", new HashSet<CodigosError>() },
			{ "FechaInicio", new HashSet<CodigosError>() },
			{ "IdGerente", new HashSet<CodigosError>() },
			{ "IdRevisor", new HashSet<CodigosError>() },
			{ "Gustan", new HashSet<CodigosError>() },
			{ "NoGustan", new HashSet<CodigosError>() },
			{ "IdEstatusPublicacion", new HashSet<CodigosError>() },
			{ "IdEstatusProyecto", new HashSet<CodigosError>() },
			{ "IdRevisadaPor", new HashSet<CodigosError>() },
			{ "IdImagenPrincipal", new HashSet<CodigosError>() },
			{ "IdTipoProyecto", new HashSet<CodigosError>() },
			{ "TiempoEstimado", new HashSet<CodigosError>() },
			{ "ProgresoEstimado", new HashSet<CodigosError>() },
			{ "ProgresoReal", new HashSet<CodigosError>() },
			{ "Evaluacion", new HashSet<CodigosError>() },
			{ "IdRutaProyecto", new HashSet<CodigosError>() },
			{ "IdFaseAnterior", new HashSet<CodigosError>() },
			{ "IdFaseSiguiente", new HashSet<CodigosError>() },
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
            else if (await ctx.Proyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Titulo != null)   // Cadena vacia
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Titulo != null && dto.Titulo.Length > 450)
            mensajes["Titulo"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (op == Operacion.Creacion && dto.FechaInicio == null)
            mensajes["FechaInicio"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion && dto.IdGerente == null)
            mensajes["IdGerente"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdGerente != null && await ctx.Usuarios.FindAsync(dto.IdGerente) == null)
            mensajes["IdGerente"].Add(CodigosError.ERR_ID_INEXISTENTE);

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

        if (op == Operacion.Creacion && dto.IdRevisadaPor == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdImagenPrincipal != null && await ctx.RecursosPublicaciones.FindAsync(dto.IdImagenPrincipal) == null)
            mensajes["IdImagenPrincipal"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdTipoProyecto == null)
            mensajes["IdTipoProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoProyecto != null && await ctx.Varias.FindAsync(dto.IdTipoProyecto) == null)
            mensajes["IdTipoProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdRutaProyecto == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdRutaProyecto != null && await ctx.Varias.FindAsync(dto.IdRutaProyecto) == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdFaseAnterior != null && await ctx.Varias.FindAsync(dto.IdFaseAnterior) == null)
            mensajes["IdFaseAnterior"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdFaseSiguiente != null && await ctx.Varias.FindAsync(dto.IdFaseSiguiente) == null)
            mensajes["IdFaseSiguiente"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
