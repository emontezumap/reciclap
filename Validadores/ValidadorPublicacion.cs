
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private PublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorPublicacion(PublicacionDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "Titulo", new HashSet<string>() },
			{ "Descripcion", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
			{ "Consecutivo", new HashSet<string>() },
			{ "IdPublicador", new HashSet<string>() },
			{ "Gustan", new HashSet<string>() },
			{ "NoGustan", new HashSet<string>() },
			{ "IdEstatusPublicacion", new HashSet<string>() },
			{ "IdFasePublicacion", new HashSet<string>() },
			{ "IdTipoPublicacion", new HashSet<string>() },
			{ "IdClasePublicacion", new HashSet<string>() },
			{ "IdRevisadaPor", new HashSet<string>() },
			{ "IdImagenPrincipal", new HashSet<string>() },
			{ "TiempoEstimado", new HashSet<string>() },
			{ "Posicionamiento", new HashSet<string>() },
			{ "Secuencia", new HashSet<string>() },
			{ "Vistas", new HashSet<string>() },
			{ "Evaluacion", new HashSet<string>() },
			{ "DireccionIpCreacion", new HashSet<string>() },
			{ "Dispositivo", new HashSet<string>() },
			{ "Direccion", new HashSet<string>() },
			{ "ReferenciasDireccion", new HashSet<string>() },
			{ "FechaDisponible", new HashSet<string>() },
			{ "TotalArticulos", new HashSet<string>() },
			{ "IdProyecto", new HashSet<string>() },
			{ "CostoEstimado", new HashSet<string>() },
			{ "IdMonedaCostoEstimado", new HashSet<string>() },
			{ "TipoCambioCostoEstimado", new HashSet<string>() },
			{ "CostoReal", new HashSet<string>() },
			{ "MontoInversion", new HashSet<string>() },
			{ "CostoRealTraslado", new HashSet<string>() },
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
            else if (await ctx.Publicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Titulo != null)   // Cadena vacia
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Titulo != null && dto.Titulo.Length > 200)
            mensajes["Titulo"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Consecutivo == null)
            mensajes["Consecutivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdPublicador == null)
            mensajes["IdPublicador"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdPublicador != null && await ctx.Usuarios.FindAsync(dto.IdPublicador) == null)
            mensajes["IdPublicador"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Gustan == null)
            mensajes["Gustan"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.NoGustan == null)
            mensajes["NoGustan"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdEstatusPublicacion == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdEstatusPublicacion != null && await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
            mensajes["IdEstatusPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdFasePublicacion == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdFasePublicacion != null && await ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdTipoPublicacion == null)
            mensajes["IdTipoPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoPublicacion != null && await ctx.Varias.FindAsync(dto.IdTipoPublicacion) == null)
            mensajes["IdTipoPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdClasePublicacion == null)
            mensajes["IdClasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdClasePublicacion != null && await ctx.Varias.FindAsync(dto.IdClasePublicacion) == null)
            mensajes["IdClasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdImagenPrincipal != null && await ctx.RecursosPublicaciones.FindAsync(dto.IdImagenPrincipal) == null)
            mensajes["IdImagenPrincipal"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TiempoEstimado == null)
            mensajes["TiempoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Posicionamiento == null)
            mensajes["Posicionamiento"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Vistas == null)
            mensajes["Vistas"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Evaluacion == null)
            mensajes["Evaluacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (string.IsNullOrEmpty(dto.DireccionIpCreacion))
        {
            if (op == Operacion.Creacion)
                mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.DireccionIpCreacion != null)   // Cadena vacia
                mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.DireccionIpCreacion != null && dto.DireccionIpCreacion.Length > 20)
            mensajes["DireccionIpCreacion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Dispositivo))
        {
            if (op == Operacion.Creacion)
                mensajes["Dispositivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Dispositivo != null)   // Cadena vacia
                mensajes["Dispositivo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Dispositivo != null && dto.Dispositivo.Length > 1)
            mensajes["Dispositivo"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Direccion))
        {
            if (op == Operacion.Creacion)
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Direccion != null)   // Cadena vacia
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Direccion != null && dto.Direccion.Length > 200)
            mensajes["Direccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.ReferenciasDireccion))
        {
            if (op == Operacion.Creacion)
                mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.ReferenciasDireccion != null)   // Cadena vacia
                mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.ReferenciasDireccion != null && dto.ReferenciasDireccion.Length > 100)
            mensajes["ReferenciasDireccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.TotalArticulos == null)
            mensajes["TotalArticulos"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

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

        if (op == Operacion.Creacion && dto.MontoInversion == null)
            mensajes["MontoInversion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.CostoRealTraslado == null)
            mensajes["CostoRealTraslado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
