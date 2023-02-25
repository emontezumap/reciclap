using DTOs;
using Herramientas;
using DB;

namespace Validadores;

public class ValidadorVarios : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private VariosDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorVarios(VariosDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "IdTabla", new List<string>()},
            { "Descripcion", new List<string>()},
            { "Referencia", new List<string>()},
            { "IdPadre", new List<string>()},
            { "Activo", new List<string>()},
            { "VersionAPI", new List<string>()}
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
                mensajes["Id"].Add("Se requiere el identificador (clave) del objeto a modificar");
                hayError = true;
            }
            else if (await ctx.Varias.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("El objeto especificado no existe");
                hayError = true;
            }
        }

        if (dto.IdTabla == null)
        {
            mensajes["IdTabla"].Add("Se debe especificar el Id de tabla");
            hayError = true;
        }
        else if (await ctx.Varias.FindAsync(dto.IdTabla) == null)
        {
            mensajes["IdTabla"].Add("La tabla especificada no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Descripcion))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Descripcion"].Add("Se requiere una descripción");
                hayError = true;
            }
            else if (dto.Descripcion != null)
            {
                mensajes["Descripcion"].Add("Se requiere una descripción");
                hayError = true;
            }
        }
        else if (dto.Descripcion.Length > 200)
        {
            mensajes["Descripcion"].Add("La descripción del objeto no debe exceder los 200 caracteres");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.Activo == null)
        {
            mensajes["Activo"].Add("Se requiere un valor para el campo Activo");
            hayError = true;
        }

        if (dto.Referencia != null && dto.Referencia.Length > 50)
        {
            mensajes["Referencia"].Add("La referencia no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (dto.IdPadre != null && await ctx.Varias.FindAsync(dto.IdPadre) == null)
        {
            mensajes["IdPadre"].Add("El objeto padre especificado no existe");
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