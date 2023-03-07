
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorChat : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private ChatDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorChat(ChatDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "IdPublicacion", new HashSet<string>() },
			{ "Titulo", new HashSet<string>() },
			{ "Fecha", new HashSet<string>() },
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
            else if (await ctx.Chats.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (op == Operacion.Creacion && dto.IdPublicacion == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdPublicacion != null && await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
            mensajes["IdPublicacion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Titulo != null)   // Cadena vacia
                mensajes["Titulo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Titulo != null && dto.Titulo.Length > 300)
            mensajes["Titulo"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.Fecha == null)
            mensajes["Fecha"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

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
