using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class EstatusPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public EstatusPublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<EstatusPublicacion>> TodosLosEstatusPublicacion()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.EstatusPublicaciones.ToListAsync<EstatusPublicacion>();
        }
    }

    public async Task<EstatusPublicacion?> EstatusPublicacionPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.EstatusPublicaciones.FindAsync(id);
        }
    }

    public async Task<EstatusPublicacion> CrearEstatusPublicacion(EstatusPublicacion nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.EstatusPublicaciones.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarEstatusPublicacion(EstatusPublicacion modif)
    {
        var buscado = await EstatusPublicacionPorId(modif.Id);

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

    public async Task<bool> EliminarEstatusPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await EstatusPublicacionPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarEstatusPublicacion(buscado);
            }

            return false;
        }
    }
}