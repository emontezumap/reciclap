using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;

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

    public async Task<TipoPublicacion> CrearTipoPublicacion(TipoPublicacionDTO nuevo, ClaimsPrincipal claims)
    {
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));

        TipoPublicacion tp = new TipoPublicacion()
        {
            Activo = nuevo.Activo,
            Descripcion = nuevo.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IdCreador = id,
            IdModificador = id
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.TiposPublicacion.Add(tp);
            await ctx.SaveChangesAsync();
        }

        return tp;
    }

    public async Task<bool> ModificarTipoPublicacion(TipoPublicacionDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.TiposPublicacion.FindAsync(modif.Id);

            if (buscado != null)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.IdModificador = id;

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarTipoPublicacion(Guid id, ClaimsPrincipal claims)
    {
        TipoPublicacionDTO tp = new TipoPublicacionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarTipoPublicacion(tp, claims);
    }
}