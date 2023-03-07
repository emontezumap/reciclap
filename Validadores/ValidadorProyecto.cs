
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private ProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorProyecto(ProyectoDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "Titulo", new HashSet<string>() },
			{ "Descripcion", new HashSet<string>() },
			{ "FechaInicio", new HashSet<string>() },
			{ "IdGerente", new HashSet<string>() },
			{ "IdRevisor", new HashSet<string>() },
			{ "Gustan", new HashSet<string>() },
			{ "NoGustan", new HashSet<string>() },
			{ "IdEstatusPublicacion", new HashSet<string>() },
			{ "IdEstatusProyecto", new HashSet<string>() },
			{ "IdRevisadaPor", new HashSet<string>() },
			{ "IdImagenPrincipal", new HashSet<string>() },
			{ "IdTipoProyecto", new HashSet<string>() },
			{ "TiempoEstimado", new HashSet<string>() },
			{ "ProgresoEstimado", new HashSet<string>() },
			{ "ProgresoReal", new HashSet<string>() },
			{ "Evaluacion", new HashSet<string>() },
			{ "IdRutaProyecto", new HashSet<string>() },
			{ "IdFaseAnterior", new HashSet<string>() },
			{ "IdFaseSiguiente", new HashSet<string>() },
			{ "FechaDisponible", new HashSet<string>() },
			{ "TotalArticulos", new HashSet<string>() },
			{ "CostoEstimado", new HashSet<string>() },
			{ "IdMonedaCostoEstimado", new HashSet<string>() },
			{ "TipoCambioCostoEstimado", new HashSet<string>() },
			{ "CostoReal", new HashSet<string>() },
			{ "IdMonedaCostoReal", new HashSet<string>() },
			{ "TipoCambioCostoReal", new HashSet<string>() },
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
            else if (await ctx.Proyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Titulo != null)   // Cadena vacia
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Titulo != null && dto.Titulo.Length > 450)
            mensajes["Titulo"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (op == Operacion.Creacion && dto.FechaInicio == null)
            mensajes["FechaInicio"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdGerente == null)
            mensajes["IdGerente"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdGerente != null && await ctx.Usuarios.FindAsync(dto.IdGerente) == null)
            mensajes["IdGerente"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdRevisor != null && await ctx.Usuarios.FindAsync(dto.IdRevisor) == null)
            mensajes["IdRevisor"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Gustan == null)
            mensajes["Gustan"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.NoGustan == null)
            mensajes["NoGustan"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdEstatusPublicacion == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdEstatusPublicacion != null && await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdEstatusProyecto == null)
            mensajes["IdEstatusProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdEstatusProyecto != null && await ctx.Varias.FindAsync(dto.IdEstatusProyecto) == null)
            mensajes["IdEstatusProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdImagenPrincipal != null && await ctx.RecursosPublicaciones.FindAsync(dto.IdImagenPrincipal) == null)
            mensajes["IdImagenPrincipal"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdTipoProyecto == null)
            mensajes["IdTipoProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoProyecto != null && await ctx.Varias.FindAsync(dto.IdTipoProyecto) == null)
            mensajes["IdTipoProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TiempoEstimado == null)
            mensajes["TiempoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.ProgresoEstimado == null)
            mensajes["ProgresoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.ProgresoReal == null)
            mensajes["ProgresoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Evaluacion == null)
            mensajes["Evaluacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdRutaProyecto == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdRutaProyecto != null && await ctx.Varias.FindAsync(dto.IdRutaProyecto) == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdFaseAnterior != null && await ctx.Varias.FindAsync(dto.IdFaseAnterior) == null)
            mensajes["IdFaseAnterior"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdFaseSiguiente != null && await ctx.Varias.FindAsync(dto.IdFaseSiguiente) == null)
            mensajes["IdFaseSiguiente"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TotalArticulos == null)
            mensajes["TotalArticulos"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.CostoEstimado == null)
            mensajes["CostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (string.IsNullOrEmpty(dto.IdMonedaCostoEstimado))
        {
            if (op == Operacion.Creacion)
                mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.IdMonedaCostoEstimado != null)   // Cadena vacia
                mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.IdMonedaCostoEstimado != null && dto.IdMonedaCostoEstimado.Length > 3)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdMonedaCostoEstimado == null)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdMonedaCostoEstimado != null && await ctx.Monedas.FindAsync(dto.IdMonedaCostoEstimado) == null)
            mensajes["IdMonedaCostoEstimado"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TipoCambioCostoEstimado == null)
            mensajes["TipoCambioCostoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.CostoReal == null)
            mensajes["CostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (string.IsNullOrEmpty(dto.IdMonedaCostoReal))
        {
            if (op == Operacion.Creacion)
                mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.IdMonedaCostoReal != null)   // Cadena vacia
                mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.IdMonedaCostoReal != null && dto.IdMonedaCostoReal.Length > 3)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdMonedaCostoReal == null)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdMonedaCostoReal != null && await ctx.Monedas.FindAsync(dto.IdMonedaCostoReal) == null)
            mensajes["IdMonedaCostoReal"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TipoCambioCostoReal == null)
            mensajes["TipoCambioCostoReal"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
