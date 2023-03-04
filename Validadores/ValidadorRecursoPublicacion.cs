
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorRecursoPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private RecursoPublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorRecursoPublicacion(RecursoPublicacionDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdTipoCatalogo", new HashSet<CodigosError>() },
			{ "IdCatalogo", new HashSet<CodigosError>() },
			{ "Secuencia", new HashSet<CodigosError>() },
			{ "IdTipoRecurso", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "IdUsuario", new HashSet<CodigosError>() },
			{ "Orden", new HashSet<CodigosError>() },
			{ "Nombre", new HashSet<CodigosError>() },
			{ "IdEstatusRecurso", new HashSet<CodigosError>() },
			{ "FechaExpiracion", new HashSet<CodigosError>() },
			{ "Tamano", new HashSet<CodigosError>() },
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
            else if (await ctx.RecursosPublicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (op == Operacion.Creacion && dto.IdTipoCatalogo == null)
            mensajes["IdTipoCatalogo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoCatalogo != null && await ctx.Varias.FindAsync(dto.IdTipoCatalogo) == null)
            mensajes["IdTipoCatalogo"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdCatalogo == null)
            mensajes["IdCatalogo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (op == Operacion.Creacion && dto.IdTipoRecurso == null)
            mensajes["IdTipoRecurso"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoRecurso != null && await ctx.Varias.FindAsync(dto.IdTipoRecurso) == null)
            mensajes["IdTipoRecurso"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Nombre != null && dto.Nombre.Length > 100)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (op == Operacion.Creacion && dto.IdEstatusRecurso == null)
            mensajes["IdEstatusRecurso"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdEstatusRecurso != null && await ctx.Varias.FindAsync(dto.IdEstatusRecurso) == null)
            mensajes["IdEstatusRecurso"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
