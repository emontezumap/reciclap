using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorChat : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private ChatDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorChat(ChatDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "IdPublicacion", new List<string>()},
            { "Titulo", new List<string>() },
            { "Fecha", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
            {
                mensajes["Id"].Add("Se requiere un chat");
                hayError = true;
            }
            else if (await ctx.Chats.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("El chat especificado no existe");
                hayError = true;
            }
        }

        if (dto.IdPublicacion == null)
        {
            mensajes["IdPublicacion"].Add("Se requiere una publicación");
            hayError = true;
        }
        else if (await ctx.Publicaciones.FindAsync(dto.IdPublicacion) == null)
        {
            mensajes["IdPublicacion"].Add("La publicación especificada no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Titulo"].Add("Se requiere el título del chat");
                hayError = true;
            }
            else if (dto.Titulo != null)
            {
                mensajes["Titulo"].Add("Se requiere el título del chat");
                hayError = true;
            }
        }
        else if (dto.Titulo.Length > 300)
        {
            mensajes["Titulo"].Add("El título del chat no debe exceder los 300 caracteres");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.Fecha == null)
        {
            mensajes["Fecha"].Add("Se requiere la fecha de creación del chat");
            hayError = true;
        }

        if ((op == Operacion.Creacion && dto.Activo == null))
        {
            mensajes["Activo"].Add("Se requiere un valor para el campo Activo");
            hayError = true;
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