
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorRecursoPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private RecursoPublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorRecursoPublicacion(RecursoPublicacionDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdTipoCatalogo", new HashSet<string>() },
			{ "IdCatalogo", new HashSet<string>() },
			{ "Secuencia", new HashSet<string>() },
			{ "IdTipoRecurso", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
			{ "IdUsuario", new HashSet<string>() },
			{ "Orden", new HashSet<string>() },
			{ "Nombre", new HashSet<string>() },
			{ "IdEstatusRecurso", new HashSet<string>() },
			{ "FechaExpiracion", new HashSet<string>() },
			{ "Tamano", new HashSet<string>() },
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
            else if (await ctx.RecursosPublicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdTipoCatalogo == null)
            mensajes["IdTipoCatalogo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoCatalogo != null && await ctx.Varias.FindAsync(dto.IdTipoCatalogo) == null)
            mensajes["IdTipoCatalogo"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdCatalogo == null)
            mensajes["IdCatalogo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdTipoRecurso == null)
            mensajes["IdTipoRecurso"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoRecurso != null && await ctx.Varias.FindAsync(dto.IdTipoRecurso) == null)
            mensajes["IdTipoRecurso"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Orden == null)
            mensajes["Orden"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Nombre != null && dto.Nombre.Length > 100)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdEstatusRecurso == null)
            mensajes["IdEstatusRecurso"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdEstatusRecurso != null && await ctx.Varias.FindAsync(dto.IdEstatusRecurso) == null)
            mensajes["IdEstatusRecurso"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Tamano == null)
            mensajes["Tamano"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
