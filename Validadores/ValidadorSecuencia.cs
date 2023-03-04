
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorSecuencia : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private SecuenciaDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorSecuencia(SecuenciaDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "Prefijo", new HashSet<CodigosError>() },
			{ "Serie", new HashSet<CodigosError>() },
			{ "Incremento", new HashSet<CodigosError>() },
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
            else if (await ctx.Secuencias.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Prefijo))
        {
            if (op == Operacion.Creacion)
                mensajes["Prefijo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Prefijo != null)   // Cadena vacia
                mensajes["Prefijo"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Prefijo != null && dto.Prefijo.Length > 10)
            mensajes["Prefijo"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

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
