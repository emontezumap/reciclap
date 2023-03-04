
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorMoneda : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private MonedaDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorMoneda(MonedaDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "Nombre", new HashSet<CodigosError>() },
			{ "TipoCambio", new HashSet<CodigosError>() },
			{ "EsLocal", new HashSet<CodigosError>() },
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
            else if (await ctx.Monedas.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Nombre != null && dto.Nombre.Length > 20)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

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
