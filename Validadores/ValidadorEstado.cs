using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorEstado : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private EstadoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorEstado(EstadoDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Nombre", new List<string>()},
            { "IdPais", new List<string>() },
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
                mensajes["Id"].Add("Se requiere un comentario");
                hayError = true;
            }
            else if (await ctx.Estados.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("El estado especificado no existe");
                hayError = true;
            }
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del estado");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del estado");
                hayError = true;
            }
        }
        else if (dto.Nombre.Length > 100)
        {
            mensajes["Nombre"].Add("El nombre del estado no debe exceder los 100 caracteres");
            hayError = true;
        }

        if (op == Operacion.Creacion)
        {
            if (dto.IdPais == null)
            {
                mensajes["IdPais"].Add("Se requiere el país");
                hayError = true;
            }
            else if (await ctx.Paises.FindAsync(dto.IdPais) == null)
            {
                mensajes["IdPais"].Add("El país especificado no existe");
                hayError = true;
            }
        }
        else if (dto.IdPais != null && await ctx.Paises.FindAsync(dto.IdPais) == null)
        {
            mensajes["IdPais"].Add("El país especificado no existe");
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