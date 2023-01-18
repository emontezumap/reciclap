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

    public async Task<TipoPublicacion> CrearTipoPublicacion(TipoPublicacionDTO nuevo)
    {
        TipoPublicacion tp = new TipoPublicacion()
        {
            Activo = nuevo.Activo,
            Descripcion = nuevo.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            // IdCreador = 
            // IdModificador = 
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.TiposPublicacion.Add(tp);
            await ctx.SaveChangesAsync();
        }

        return tp;
    }

    public async Task<bool> ModificarTipoPublicacion(TipoPublicacionDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.TiposPublicacion.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.FechaCreacion = DateTime.UtcNow;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.Id = Guid.NewGuid();
                // IdCreador = 
                // IdModificador = 

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarTipoPublicacion(Guid id)
    {
        TipoPublicacionDTO tp = new TipoPublicacionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarTipoPublicacion(tp);
    }
}