using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

namespace Services;

[ExtendObjectType("Mutacion")]
public class TipoPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public TipoPublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<TipoPublicacion>> TodosLosTiposPublicacion()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.TiposPublicacion.ToListAsync<TipoPublicacion>();
        }
    }

    public async Task<TipoPublicacion?> UnTipoPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.TiposPublicacion.FindAsync(id);
        }
    }

    public async Task<TipoPublicacion> CrearTipoPublicacion(TipoPublicacion nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.TiposPublicacion.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarTipoPublicacion(TipoPublicacion modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await UnTipoPublicacion(modif.Id);

            if (buscado != null)
            {
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarTipoPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await UnTipoPublicacion(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarTipoPublicacion(buscado);
            }

            return false;
        }
    }
}