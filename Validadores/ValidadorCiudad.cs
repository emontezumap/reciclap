using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorCiudad : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private CiudadDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorCiudad(CiudadDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Nombre", new List<string>()},
            { "IdEstado", new List<string>() },
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion && await ctx.Ciudades.FindAsync(dto.Id) == null)
        {
            mensajes["Id"].Add("La Ciudad especificada no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Nombre))
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el nombre de la ciudad");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Nombre"].Add("Se requiere el nombre de la ciudad");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Nombre) && dto.Nombre.Length > 100)
        {
            mensajes["Nombre"].Add("El nombre de la ciudad no debe exceder los 100 caracteres");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.IdEstado == null)
        {
            mensajes["IdEstado"].Add("Se requiere el Estado");
            hayError = true;
        }

        if (dto.IdEstado != null && await ctx.Ciudades.FindAsync(dto.IdEstado) == null)
        {
            mensajes["IdEstado"].Add("El Estado especificado no existe");
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