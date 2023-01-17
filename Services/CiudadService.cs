using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class CiudadService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public CiudadService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Ciudad>> TodasLasCiudades()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.ToListAsync<Ciudad>();
        }
    }

    public async Task<Ciudad?> CiudadPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.FindAsync(id);
        }
    }

    public async Task<Ciudad> CrearCiudad(Ciudad nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Ciudades.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarCiudad(Ciudad modif)
    {
        var buscado = await CiudadPorId(modif.Id);

        if (buscado != null)
        {
            using (var ctx = ctxFactory.CreateDbContext())
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarCiudad(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await CiudadPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarCiudad(buscado);
            }

            return false;
        }
    }
}