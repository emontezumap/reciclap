using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPais : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private PaisDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorPais(PaisDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Nombre", new List<string>()},
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
                mensajes["Id"].Add("Se requiere el país a modificar");
                hayError = true;
            }
            else if (await ctx.Paises.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("El país especificado no existe");
                hayError = true;
            }
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del país");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del país");
                hayError = true;
            }
        }
        else if (dto.Nombre.Length > 50)
        {
            mensajes["Nombre"].Add("El nombre del país no debe exceder los 50 caracteres");
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