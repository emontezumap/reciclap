using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class EstatusPublicacionService
{
    private readonly SSDBContext ctx;

    public EstatusPublicacionService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<EstatusPublicacion>> Todos()
    {
        return await ctx.EstatusPublicaciones.ToListAsync<EstatusPublicacion>();
    }

    public async Task<EstatusPublicacion?> PorId(Guid id)
    {
        return await ctx.EstatusPublicaciones.FindAsync(id);
    }

    public async Task<EstatusPublicacion> Crear(EstatusPublicacion nuevo)
    {
        ctx.EstatusPublicaciones.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(EstatusPublicacion modif)
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