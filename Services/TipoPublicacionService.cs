using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class TipoPublicacionService
{
    private readonly SSDBContext ctx;

    public TipoPublicacionService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<TipoPublicacion>> Todos()
    {
        return await ctx.TiposPublicacion.ToListAsync<TipoPublicacion>();
    }

    public async Task<TipoPublicacion?> PorId(Guid id)
    {
        return await ctx.TiposPublicacion.FindAsync(id);
    }

    public async Task<TipoPublicacion> Crear(TipoPublicacion nuevo)
    {
        ctx.TiposPublicacion.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(TipoPublicacion modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Descripcion = modif.Descripcion;
            buscado.IdModificador = modif.IdModificador;
            buscado.FechaModificacion = DateTime.UtcNow;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            buscado.Activo = false;
            await ctx.SaveChangesAsync();
        }
    }
}