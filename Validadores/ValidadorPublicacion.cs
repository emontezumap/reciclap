
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private PublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorPublicacion(PublicacionDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "Titulo", new HashSet<CodigosError>() },
			{ "Descripcion", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "Consecutivo", new HashSet<CodigosError>() },
			{ "IdPublicador", new HashSet<CodigosError>() },
			{ "Gustan", new HashSet<CodigosError>() },
			{ "NoGustan", new HashSet<CodigosError>() },
			{ "IdEstatusPublicacion", new HashSet<CodigosError>() },
			{ "IdFasePublicacion", new HashSet<CodigosError>() },
			{ "IdTipoPublicacion", new HashSet<CodigosError>() },
			{ "IdClasePublicacion", new HashSet<CodigosError>() },
			{ "IdRevisadaPor", new HashSet<CodigosError>() },
			{ "IdImagenPrincipal", new HashSet<CodigosError>() },
			{ "TiempoEstimado", new HashSet<CodigosError>() },
			{ "Posicionamiento", new HashSet<CodigosError>() },
			{ "Secuencia", new HashSet<CodigosError>() },
			{ "Vistas", new HashSet<CodigosError>() },
			{ "Evaluacion", new HashSet<CodigosError>() },
			{ "DireccionIpCreacion", new HashSet<CodigosError>() },
			{ "Dispositivo", new HashSet<CodigosError>() },
			{ "Direccion", new HashSet<CodigosError>() },
			{ "ReferenciasDireccion", new HashSet<CodigosError>() },
			{ "FechaDisponible", new HashSet<CodigosError>() },
			{ "TotalArticulos", new HashSet<CodigosError>() },
			{ "IdProyecto", new HashSet<CodigosError>() },
			{ "CostoEstimado", new HashSet<CodigosError>() },
			{ "IdMonedaCostoEstimado", new HashSet<CodigosError>() },
			{ "TipoCambioCostoEstimado", new HashSet<CodigosError>() },
			{ "CostoReal", new HashSet<CodigosError>() },
			{ "MontoInversion", new HashSet<CodigosError>() },
			{ "CostoRealTraslado", new HashSet<CodigosError>() },
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
            else if (await ctx.Publicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Titulo != null)   // Cadena vacia
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Titulo != null && dto.Titulo.Length > 200)
            mensajes["Titulo"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (op == Operacion.Creacion && dto.Consecutivo == null)
            mensajes["Consecutivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion && dto.IdPublicador == null)
            mensajes["IdPublicador"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdPublicador != null && await ctx.Usuarios.FindAsync(dto.IdPublicador) == null)
            mensajes["IdPublicador"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdEstatusPublicacion == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdEstatusPublicacion != null && await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdFasePublicacion == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdFasePublicacion != null && await ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdTipoPublicacion == null)
            mensajes["IdTipoPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoPublicacion != null && await ctx.Varias.FindAsync(dto.IdTipoPublicacion) == null)
            mensajes["IdTipoPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdClasePublicacion == null)
            mensajes["IdClasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdClasePublicacion != null && await ctx.Varias.FindAsync(dto.IdClasePublicacion) == null)
            mensajes["IdClasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdImagenPrincipal != null && await ctx.RecursosPublicaciones.FindAsync(dto.IdImagenPrincipal) == null)
            mensajes["IdImagenPrincipal"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (string.IsNullOrEmpty(dto.DireccionIpCreacion))
        {
            if (op == Operacion.Creacion)
                mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.DireccionIpCreacion != null)   // Cadena vacia
                mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.DireccionIpCreacion != null && dto.DireccionIpCreacion.Length > 20)
            mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Dispositivo))
        {
            if (op == Operacion.Creacion)
                mensajes["Dispositivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Dispositivo != null)   // Cadena vacia
                mensajes["Dispositivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Dispositivo != null && dto.Dispositivo.Length > 1)
            mensajes["Dispositivo"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Direccion))
        {
            if (op == Operacion.Creacion)
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Direccion != null)   // Cadena vacia
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Direccion != null && dto.Direccion.Length > 200)
            mensajes["Direccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.ReferenciasDireccion))
        {
            if (op == Operacion.Creacion)
                mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.ReferenciasDireccion != null)   // Cadena vacia
                mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.ReferenciasDireccion != null && dto.ReferenciasDireccion.Length > 100)
            mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (!SinReferencias && dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
