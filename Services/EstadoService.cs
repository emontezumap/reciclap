using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class EstadoService
{
    private readonly SSDBContext ctx;

    public EstadoService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Estado>> Todos()
    {
        return await ctx.Estados.ToListAsync<Estado>();
    }

    public async Task<Estado?> PorId(Guid id)
    {
        return await ctx.Estados.FindAsync(id);
    }

    public async Task<Estado> Crear(Estado nuevo)
    {
        ctx.Estados.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Estado modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Nombre = modif.Nombre;
            buscado.IdPais = modif.IdPais;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Estados.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}