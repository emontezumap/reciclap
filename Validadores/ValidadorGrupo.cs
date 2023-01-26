using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorGrupo : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private GrupoDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorGrupo(GrupoDTO dto, Operacion op, SSDBContext ctx)
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

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
            {
                mensajes["Id"].Add("Se requiere el grupo de usuarios a modificar");
                hayError = true;
            }
            else if (await ctx.Grupos.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("El grupo de usuarios especificado no existe");
                hayError = true;
            }
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
        else if (dto.Descripcion.Length > 100)
        {
            mensajes["Descripcion"].Add("La descripción del grupo de usuarios no debe exceder los 100 caracteres");
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