
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorVarios : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private VariosDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorVarios(VariosDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdTabla", new HashSet<string>() },
			{ "Descripcion", new HashSet<string>() },
			{ "Referencia", new HashSet<string>() },
			{ "IdPadre", new HashSet<string>() },
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
            else if (await ctx.Varias.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.IdTabla))
        {
            if (op == Operacion.Creacion)
                mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.IdTabla != null)   // Cadena vacia
                mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.IdTabla != null && dto.IdTabla.Length > 10)
            mensajes["IdTabla"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdTabla == null)
            mensajes["IdTabla"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTabla != null && await ctx.Tablas.FindAsync(dto.IdTabla) == null)
            mensajes["IdTabla"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Descripcion != null)   // Cadena vacia
                mensajes["Descripcion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Descripcion != null && dto.Descripcion.Length > 1000)
            mensajes["Descripcion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.Referencia != null && dto.Referencia.Length > 50)
            mensajes["Referencia"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (ValidarReferencias && dto.IdPadre != null && await ctx.Varias.FindAsync(dto.IdPadre) == null)
            mensajes["IdPadre"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

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
