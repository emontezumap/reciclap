using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;

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

    public async Task<EstatusPublicacion?> UnEstatusPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.EstatusPublicaciones.FindAsync(id);
        }
    }

    public async Task<EstatusPublicacion> CrearEstatusPublicacion(EstatusPublicacionDTO nuevo, ClaimsPrincipal claims)
    {
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));

        EstatusPublicacion ep = new EstatusPublicacion()
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
            ctx.EstatusPublicaciones.Add(ep);
            await ctx.SaveChangesAsync();
        }

        return ep;
    }

    public async Task<bool> ModificarEstatusPublicacion(EstatusPublicacionDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.EstatusPublicaciones.FindAsync(modif.Id);

            if (buscado != null)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.FechaCreacion = DateTime.UtcNow;
                buscado.IdModificador = id;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarEstatusPublicacion(Guid id, ClaimsPrincipal claims)
    {
        EstatusPublicacionDTO ep = new EstatusPublicacionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarEstatusPublicacion(ep, claims);
    }
}