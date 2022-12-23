using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class PaisService
{
    private readonly SSDBContext ctx;

    public PaisService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Pais>> Todos()
    {
        return await ctx.Paises.ToListAsync<Pais>();
    }

    public async Task<Pais?> PorId(Guid id)
    {
        return await ctx.Paises.FindAsync(id);
    }

    public async Task<Pais> Crear(Pais nuevo)
    {
        ctx.Paises.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Pais modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Nombre = modif.Nombre;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Paises.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}