using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class PublicacionService
{
    private readonly SSDBContext ctx;

    public PublicacionService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Publicacion>> Todos()
    {
        return await ctx.Publicaciones.ToListAsync<Publicacion>();
    }

    public async Task<Publicacion?> PorId(Guid id)
    {
        return await ctx.Publicaciones.FindAsync(id);
    }

    public async Task<Publicacion> Crear(Publicacion nuevo)
    {
        ctx.Publicaciones.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Publicacion modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Titulo = modif.Titulo;
            buscado.Descripcion = modif.Descripcion;
            buscado.Fecha = modif.Fecha;
            buscado.Gustan = modif.Gustan;
            buscado.NoGustan = modif.NoGustan;
            buscado.IdEstatus = modif.IdEstatus;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Publicaciones.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}