using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class RolService
{
    private readonly SSDBContext ctx;

    public RolService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Rol>> Todos()
    {
        return await ctx.Roles.ToListAsync<Rol>();
    }

    public async Task<Rol?> PorId(Guid id)
    {
        return await ctx.Roles.FindAsync(id);
    }

    public async Task<Rol> Crear(Rol nuevo)
    {
        ctx.Roles.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Rol modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Descripcion = modif.Descripcion;
            buscado.EsCreador = modif.EsCreador;
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