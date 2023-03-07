
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorBitacoraProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private BitacoraProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorBitacoraProyecto(BitacoraProyectoDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdProyecto", new HashSet<string>() },
			{ "IdActividadProyecto", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
			{ "IdUsuario", new HashSet<string>() },
			{ "IdTipoBitacora", new HashSet<string>() },
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
            else if (await ctx.BitacorasProyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdProyecto == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdProyecto != null && await ctx.Proyectos.FindAsync(dto.IdProyecto) == null)
            mensajes["IdProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdActividadProyecto == null)
            mensajes["IdActividadProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdActividadProyecto != null && await ctx.ActividadesProyectos.FindAsync(dto.IdActividadProyecto) == null)
            mensajes["IdActividadProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.IdUsuario == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdUsuario != null && await ctx.Usuarios.FindAsync(dto.IdUsuario) == null)
            mensajes["IdUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdTipoBitacora == null)
            mensajes["IdTipoBitacora"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoBitacora != null && await ctx.Varias.FindAsync(dto.IdTipoBitacora) == null)
            mensajes["IdTipoBitacora"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Comentarios))
        {
            if (op == Operacion.Creacion)
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Comentarios != null)   // Cadena vacia
                mensajes["Comentarios"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

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
