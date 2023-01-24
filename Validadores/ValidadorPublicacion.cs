using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorPublicacion : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private PublicacionDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorPublicacion(PublicacionDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Titulo", new List<string>()},
            { "Descripcion", new List<string>()},
            { "Fecha", new List<string>()},
            { "Gustan", new List<string>()},
            { "NoGustan", new List<string>()},
            { "IdStatus", new List<string>() },
            { "IdTipoPublicacion", new List<string>()},
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
                mensajes["Id"].Add("Se requiere la publicación a modificar");
                hayError = true;
            }
            else if (await ctx.Publicaciones.FindAsync(dto.Id) == null)
            {
                mensajes["Id"].Add("La publicación especificada no existe");
                hayError = true;
            }
        }

        if (string.IsNullOrEmpty(dto.Titulo))
        {
            if (op == Operacion.Creacion)
            {
                mensajes["Titulo"].Add("Se requiere el título de la publicación");
                hayError = true;
            }
            else if (dto.Titulo != null)
            {
                mensajes["Titulo"].Add("Se requiere el título de la publicación");
                hayError = true;
            }
        }
        else if (dto.Titulo.Length > 200)
        {
            mensajes["Titulo"].Add("El título de la publicación no debe exceder los 200 caracteres");
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

        if (op == Operacion.Creacion && dto.Fecha == null)
        {
            mensajes["Fecha"].Add("Se requiere la fecha de asignación del personal");
            hayError = true;
        }

        if (op == Operacion.Creacion)
        {
            if (dto.IdEstatus == null)
            {
                mensajes["IdEstatus"].Add("Se requiere el estatus de la publicación");
                hayError = true;
            }
            else if (await ctx.EstatusPublicaciones.FindAsync(dto.IdEstatus) == null)
            {
                mensajes["IdEstatus"].Add("El estatus especificado no existe");
                hayError = true;
            }
        }
        else if (dto.IdEstatus != null && await ctx.EstatusPublicaciones.FindAsync(dto.IdEstatus) == null)
        {
            mensajes["IdEstatus"].Add("El estatus especificado no existe");
            hayError = true;
        }

        if (op == Operacion.Creacion)
        {
            if (dto.IdTipoPublicacion == null)
            {
                mensajes["IdTipoPublicacion"].Add("Se requiere el tipo de publicación");
                hayError = true;
            }
            else if (await ctx.TiposPublicacion.FindAsync(dto.IdTipoPublicacion) == null)
            {
                mensajes["IdTipoPublicacion"].Add("El tipo de publicación especificado no existe");
                hayError = true;
            }
        }
        else if (dto.IdTipoPublicacion != null && await ctx.TiposPublicacion.FindAsync(dto.IdTipoPublicacion) == null)
        {
            mensajes["IdTipoPublicacion"].Add("El tipo de publicación especificado no existe");
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