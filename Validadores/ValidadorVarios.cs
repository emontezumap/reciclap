
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorVarios : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private VariosDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorVarios(VariosDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "IdTabla", new HashSet<CodigosError>() },
			{ "Descripcion", new HashSet<CodigosError>() },
			{ "Referencia", new HashSet<CodigosError>() },
			{ "IdPadre", new HashSet<CodigosError>() },
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
            else if (await ctx.Varias.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.IdTabla))
        {
            if (op == Operacion.Creacion)
                mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.IdTabla != null)   // Cadena vacia
                mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.IdTabla != null && dto.IdTabla.Length > 10)
            mensajes["IdTabla"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (op == Operacion.Creacion && dto.IdTabla == null)
            mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTabla != null && await ctx.Tablas.FindAsync(dto.IdTabla) == null)
            mensajes["IdTabla"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Descripcion != null && dto.Descripcion.Length > 1000)
            mensajes["Descripcion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Referencia))
        {
            if (op == Operacion.Creacion)
                mensajes["Referencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Referencia != null)   // Cadena vacia
                mensajes["Referencia"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Referencia != null && dto.Referencia.Length > 50)
            mensajes["Referencia"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (!SinReferencias && dto.IdPadre != null && await ctx.Varias.FindAsync(dto.IdPadre) == null)
            mensajes["IdPadre"].Add(CodigosError.ERR_ID_INEXISTENTE);

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
