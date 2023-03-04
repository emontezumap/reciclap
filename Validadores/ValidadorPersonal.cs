
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPersonal : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private PersonalDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorPersonal(PersonalDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "IdPublicacion", new HashSet<CodigosError>() },
			{ "IdUsuario", new HashSet<CodigosError>() },
			{ "Fecha", new HashSet<CodigosError>() },
			{ "IdRol", new HashSet<CodigosError>() },
			{ "Activo", new HashSet<CodigosError>() }
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
        this.SinReferencias = SinReferencias;
    }

    public async Task<ResultadoValidacion> Validar()
    {

        if (op == Operacion.Creacion && dto.IdPublicacion == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdPublicacion != null && await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (op == Operacion.Creacion && dto.IdRol == null)
            mensajes["IdRol"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdRol != null && await ctx.Varias.FindAsync(dto.IdRol) == null)
            mensajes["IdRol"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
