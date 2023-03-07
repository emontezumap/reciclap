
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorMoneda : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private MonedaDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorMoneda(MonedaDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "Nombre", new HashSet<string>() },
			{ "TipoCambio", new HashSet<string>() },
			{ "EsLocal", new HashSet<string>() },
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
            else if (await ctx.Monedas.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Nombre != null && dto.Nombre.Length > 20)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.TipoCambio == null)
            mensajes["TipoCambio"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
