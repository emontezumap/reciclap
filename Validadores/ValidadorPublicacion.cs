using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<Error>> mensajes;
    private PublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorPublicacion(PublicacionDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, HashSet<Error>>() {
            { "Id", new HashSet<Error>() },
            { "Titulo", new HashSet<Error>()},
            { "Descripcion", new HashSet<Error>()},
            { "Fecha", new HashSet<Error>()},
            { "Consecutivo", new HashSet<Error>()},
            { "IdPublicador", new HashSet<Error>()},
            { "Gustan", new HashSet<Error>()},
            { "NoGustan", new HashSet<Error>()},
            { "IdEstatusPublicacion", new HashSet<Error>() },
            { "IdFasePublicacion", new HashSet<Error>() },
            { "IdTipoPublicacion", new HashSet<Error>()},
            { "IdClasePublicacion", new HashSet<Error>()},
            { "IdRevisadaPor", new HashSet<Error>()},
            { "IdImagenPrincipal", new HashSet<Error>()},
            { "TiempoEstimado", new HashSet<Error>()},
            { "Posicionamiento", new HashSet<Error>()},
            { "Secuencia", new HashSet<Error>()},
            { "Vistas", new HashSet<Error>()},
            { "Evaluacion", new HashSet<Error>()},
            { "DireccionIPCreacion", new HashSet<Error>()},
            { "Dispositivo", new HashSet<Error>()},
            { "Direccion", new HashSet<Error>()},
            { "ReferenciaDireccion", new HashSet<Error>()},
            { "FechaDisponible", new HashSet<Error>()},
            { "TotalArticulos", new HashSet<Error>()},
            { "IdProyecto", new HashSet<Error>()},
            { "CostoEstimado", new HashSet<Error>()},
            { "IdMonedaCostoEstimado", new HashSet<Error>()},
            { "TipoCambioCostoEstimado", new HashSet<Error>()},
            { "CostoReal", new HashSet<Error>()},
            { "MontoInversion", new HashSet<Error>()},
            { "CostoRealTraslado", new HashSet<Error>()},
            { "IdMonedaCostoReal", new HashSet<Error>()},
            { "TipoCambioCostoReal", new HashSet<Error>()},
            { "Activo", new HashSet<Error>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
                mensajes["Id"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Publicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(Error.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (dto.Titulo != null)
                mensajes["Titulo"].Add(Error.ERR_CAMPO_REQUERIDO);
        }
        else if (dto.Titulo.Length > 200)
            mensajes["Titulo"].Add(Error.ERR_CADENA_MUY_LARGA);

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)
                mensajes["Descripcion"].Add(Error.ERR_CAMPO_REQUERIDO);
        }

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(Error.ERR_CAMPO_REQUERIDO);

        // IdPublicador
        if (op == Operacion.Creacion)
        {
            if (dto.IdPublicador == null)
                mensajes["IdPublicador"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Usuarios.FindAsync(dto.IdPublicador) == null)
                mensajes["IdPublicador"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdPublicador != null && await ctx.Usuarios.FindAsync(dto.IdPublicador) == null)
            mensajes["IdPublicador"].Add(Error.ERR_ID_INEXISTENTE);

        // Gustan
        if (op == Operacion.Creacion && dto.Gustan == null)
            mensajes["Gustan"].Add(Error.ERR_CAMPO_REQUERIDO);

        // NoGustan
        if (op == Operacion.Creacion && dto.NoGustan == null)
            mensajes["NoGustan"].Add(Error.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion)
        {
            if (dto.IdEstatusPublicacion == null)
                mensajes["IdEstatusPublicacion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
                mensajes["IdEstatusPublicacion"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdEstatusPublicacion != null && await ctx.Varias.FindAsync(dto.IdEstatusPublicacion) == null)
            mensajes["IdEstatusPublicacion"].Add(Error.ERR_ID_INEXISTENTE);

        // IdFasePublicacion
        if (op == Operacion.Creacion)
        {
            if (dto.IdFasePublicacion == null)
                mensajes["IdFasePublicacion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
                mensajes["IdFasePublicacion"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdFasePublicacion != null && ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
            mensajes["IdFasePublicacion"].Add(Error.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion)
        {
            if (dto.IdTipoPublicacion == null)
                mensajes["IdTipoPublicacion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Varias.FindAsync(dto.IdTipoPublicacion) == null)
                mensajes["IdTipoPublicacion"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdTipoPublicacion != null && await ctx.Varias.FindAsync(dto.IdTipoPublicacion) == null)
            mensajes["IdTipoPublicacion"].Add(Error.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion)
        {
            if (dto.IdClasePublicacion == null)
                mensajes["IdClasePublicacion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Varias.FindAsync(dto.IdClasePublicacion) == null)
                mensajes["IdClasePublicacion"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdClasePublicacion != null && await ctx.Varias.FindAsync(dto.IdClasePublicacion) == null)
            mensajes["IdClasePublicacion"].Add(Error.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion)
        {
            if (dto.IdRevisadaPor == null)
                mensajes["IdRevisadaPor"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
                mensajes["IdRevisadaPor"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdRevisadaPor != null && await ctx.Usuarios.FindAsync(dto.IdRevisadaPor) == null)
            mensajes["IdRevisadaPor"].Add(Error.ERR_ID_INEXISTENTE);

        if (dto.IdImagenPrincipal != null && await ctx.Usuarios.FindAsync(dto.IdImagenPrincipal) == null)
            mensajes["IdImagenPrincipal"].Add(Error.ERR_ID_INEXISTENTE);

        // TiempoEstimado
        if (op == Operacion.Creacion && dto.TiempoEstimado == null)
            mensajes["TiempoEstimado"].Add(Error.ERR_CAMPO_REQUERIDO);

        // Posicionamiento
        if (op == Operacion.Creacion && dto.Posicionamiento == null)
            mensajes["Posicionamiento"].Add(Error.ERR_CAMPO_REQUERIDO);

        // Secuencia
        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(Error.ERR_CAMPO_REQUERIDO);

        // Vistas
        if (op == Operacion.Creacion && dto.Vistas == null)
            mensajes["Vistas"].Add(Error.ERR_CAMPO_REQUERIDO);

        // Evaluacion
        if (op == Operacion.Creacion && dto.Evaluacion == null)
            mensajes["Evaluacion"].Add(Error.ERR_CAMPO_REQUERIDO);

        if (dto.Evaluacion != null && (dto.Evaluacion < 0.1M || dto.Evaluacion > 5.0M))
            mensajes["Evaluacion"].Add(Error.ERR_VALOR_FUERA_DE_RANGO);

        if (string.IsNullOrEmpty(dto.DireccionIPCreacion))
        {
            if (op == Operacion.Creacion)
                mensajes["DireccionIPCreacion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (dto.DireccionIPCreacion != null)
                mensajes["DireccionIPCreacion"].Add(Error.ERR_CAMPO_REQUERIDO);
        }
        else if (dto.DireccionIPCreacion.Length > 20)
            mensajes["DireccionIPCreacion"].Add(Error.ERR_CADENA_MUY_LARGA);

        if (string.IsNullOrEmpty(dto.Dispositivo))
        {
            if (op == Operacion.Creacion)
                mensajes["Dispositivo"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (dto.Dispositivo != null)
                mensajes["Dispositivo"].Add(Error.ERR_CAMPO_REQUERIDO);
        }
        else if (dto.Dispositivo.Length > 1)
            mensajes["Dispositivo"].Add(Error.ERR_CADENA_MUY_LARGA);

        if (string.IsNullOrEmpty(dto.Direccion))
        {
            if (op == Operacion.Creacion)
                mensajes["Direccion"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (dto.Direccion != null)
                mensajes["Direccion"].Add(Error.ERR_CAMPO_REQUERIDO);
        }
        else if (dto.Direccion.Length > 1)
            mensajes["Direccion"].Add(Error.ERR_CADENA_MUY_LARGA);

        if (!string.IsNullOrEmpty(dto.ReferenciasDireccion) && dto.ReferenciasDireccion.Length > 100)
            mensajes["ReferenciasDireccion"].Add(Error.ERR_CADENA_MUY_LARGA);

        if (op == Operacion.Creacion && dto.FechaDisponible == null)
            mensajes["FechaDisponible"].Add(Error.ERR_CAMPO_REQUERIDO);

        // TotalArticulos
        if (op == Operacion.Creacion && dto.TotalArticulos == null)
            mensajes["TotalArticulos"].Add(Error.ERR_CAMPO_REQUERIDO);

        if (dto.TotalArticulos != null && (dto.TotalArticulos < 0.0M))
            mensajes["TotalArticulos"].Add(Error.ERR_VALOR_FUERA_DE_RANGO);

        // CostoEstimado
        if (op == Operacion.Creacion && dto.CostoEstimado == null)
            mensajes["CostoEstimado"].Add(Error.ERR_CAMPO_REQUERIDO);

        if (dto.CostoEstimado != null && (dto.CostoEstimado < 0.0M))
            mensajes["CostoEstimado"].Add(Error.ERR_VALOR_FUERA_DE_RANGO);

        if (op == Operacion.Creacion)
        {
            if (dto.IdPublicador == null)
                mensajes["IdProyecto"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
                mensajes["IdProyecto"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(Error.ERR_ID_INEXISTENTE);

        // IdMonedaCostoEstimado
        if (op == Operacion.Creacion)
        {
            if (dto.IdMonedaCostoEstimado == null)
                mensajes["IdMonedaCostoEstimado"].Add(Error.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Varias.FindAsync(dto.IdMonedaCostoEstimado) == null)
                mensajes["IdMonedaCostoEstimado"].Add(Error.ERR_ID_INEXISTENTE);
        }
        else if (dto.IdMonedaCostoEstimado != null && await ctx.Varias.FindAsync(dto.IdMonedaCostoEstimado) == null)
            mensajes["IdMonedaCostoEstimado"].Add(Error.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.Activo == null)
            mensajes["Activo"].Add(Error.ERR_CAMPO_REQUERIDO);

        bool hayError = false;

        foreach (KeyValuePair<string, HashSet<Error>> entry in mensajes)
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