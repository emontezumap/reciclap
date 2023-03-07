
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorRastreoPublicacion : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private RastreoPublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorRastreoPublicacion(RastreoPublicacionDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdPublicacion", new HashSet<string>() },
			{ "IdFasePublicacion", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
			{ "IdUsuario", new HashSet<string>() },
			{ "TiempoEstimado", new HashSet<string>() },
			{ "IdFaseAnterior", new HashSet<string>() },
			{ "IdFaseSiguiente", new HashSet<string>() },
			{ "Comentarios", new HashSet<string>() },
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
            else if (await ctx.RastreoPublicaciones.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdPublicacion == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdPublicacion != null && await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdFasePublicacion == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdFasePublicacion != null && await ctx.Varias.FindAsync(dto.IdFasePublicacion) == null)
            mensajes["IdFasePublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.TiempoEstimado == null)
            mensajes["TiempoEstimado"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdFaseAnterior != null && await ctx.Varias.FindAsync(dto.IdFaseAnterior) == null)
            mensajes["IdFaseAnterior"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdFaseSiguiente != null && await ctx.Varias.FindAsync(dto.IdFaseSiguiente) == null)
            mensajes["IdFaseSiguiente"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Comentarios))
        {
            if (op == Operacion.Creacion)
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Comentarios != null)   // Cadena vacia
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Comentarios != null && dto.Comentarios.Length > 100)
            mensajes["Comentarios"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

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
