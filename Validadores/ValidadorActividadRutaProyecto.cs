
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorActividadRutaProyecto : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private ActividadRutaProyectoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorActividadRutaProyecto(ActividadRutaProyectoDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdRutaProyecto", new HashSet<string>() },
			{ "Descripcion", new HashSet<string>() },
			{ "Secuencia", new HashSet<string>() },
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
            else if (await ctx.ActividadesRutasProyectos.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdRutaProyecto == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdRutaProyecto != null && await ctx.Varias.FindAsync(dto.IdRutaProyecto) == null)
            mensajes["IdRutaProyecto"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Descripcion != null && dto.Descripcion.Length > 450)
            mensajes["Descripcion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.Secuencia == null)
            mensajes["Secuencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
