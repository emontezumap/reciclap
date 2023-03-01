using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorSecuencia : IValidadorEntidad
{

    private Dictionary<string, List<string>> mensajes;
    private SecuenciaDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorSecuencia(SecuenciaDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Prefijo", new List<string>()},
            { "Serie", new List<string>()},
            { "Incremento", new List<string>()},
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
                mensajes["Id"].Add("Se requiere el Id de la secuencia a modificar");
                hayError = true;
            }
            else if (await ctx.Secuencias.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("La secuencia especificada no existe");
                hayError = true;
            }
        }

        if (string.IsNullOrEmpty(dto.Prefijo))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Titulo"].Add("Se requiere el prefijo de la secuencia");
                hayError = true;
            }
            else if (dto.Prefijo != null)
            {
                mensajes["Titulo"].Add("Se requiere el prefijo de la secuencia");
                hayError = true;
            }
        }
        else if (dto.Prefijo.Length > 10)
        {
            mensajes["Titulo"].Add("El prefijo de la secuencia no debe exceder los 10 caracteres");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.Activo == null)
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