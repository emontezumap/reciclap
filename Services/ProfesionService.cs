using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class ProfesionService
{
    private readonly SSDBContext ctx;

    public ProfesionService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Profesion>> Todos()
    {
        return await ctx.Profesiones.ToListAsync<Profesion>();
    }

    public async Task<Profesion?> PorId(Guid id)
    {
        return await ctx.Profesiones.FindAsync(id);
    }

    public async Task<Profesion> Crear(Profesion nuevo)
    {
        ctx.Profesiones.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Profesion modif)
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