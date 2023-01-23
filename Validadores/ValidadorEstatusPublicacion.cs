using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorEstatusPublicacion : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private EstatusPublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorEstatusPublicacion(EstatusPublicacionDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Descripcion", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion && await ctx.Estados.FindAsync(dto.Id) == null)
        {
            mensajes["Id"].Add("El estatus de publicación especificado no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Descripcion))
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere una descripción");
                hayError = true;
            }
            else if (dto.Descripcion != null)
            {
                mensajes["Nombre"].Add("Se requiere una descripción");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Descripcion) && dto.Descripcion.Length > 200)
        {
            mensajes["Nombre"].Add("La descripción del estatus no debe exceder los 200 caracteres");
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