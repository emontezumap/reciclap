using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class CiudadService
{
    private readonly SSDBContext ctx;

    public CiudadService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Ciudad>> Todos()
    {
        return await ctx.Ciudades.ToListAsync<Ciudad>();
    }

    public async Task<Ciudad?> PorId(Guid id)
    {
        return await ctx.Ciudades.FindAsync(id);
    }

    public async Task<Ciudad> Crear(Ciudad nuevo)
    {
        ctx.Ciudades.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Ciudad modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Nombre = modif.Nombre;
            buscado.IdEstado = modif.IdEstado;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Ciudades.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}