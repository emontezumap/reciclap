
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorSecuencia : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private SecuenciaDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorSecuencia(SecuenciaDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "Prefijo", new HashSet<string>() },
			{ "Serie", new HashSet<string>() },
			{ "Incremento", new HashSet<string>() },
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
            else if (await ctx.Secuencias.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.Prefijo))
        {
            if (op == Operacion.Creacion)
                mensajes["Prefijo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Prefijo != null)   // Cadena vacia
                mensajes["Prefijo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Prefijo != null && dto.Prefijo.Length > 10)
            mensajes["Prefijo"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.Serie == null)
            mensajes["Serie"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (op == Operacion.Creacion && dto.Incremento == null)
            mensajes["Incremento"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
