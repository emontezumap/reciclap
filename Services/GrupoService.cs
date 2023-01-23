using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;

namespace Services;

[ExtendObjectType("Mutacion")]
public class GrupoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public GrupoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Grupo>> TodosLosGrupos()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Grupos.ToListAsync<Grupo>();
        }
    }

    public async Task<Grupo?> UnGrupo(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Grupos.FindAsync(id);
        }
    }

    public async Task<Grupo> CrearGrupo(GrupoDTO nuevo, ClaimsPrincipal claims)
    {
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));

        Grupo grp = new Grupo()
        {
            Activo = nuevo.Activo,
            Descripcion = nuevo.Descripcion,
            EsAdministrador = nuevo.EsAdministrador,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IdCreador = id,
            IdModificador = id
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Grupos.Add(grp);
            await ctx.SaveChangesAsync();
        }

        return grp;
    }

    public async Task<bool> ModificarGrupo(GrupoDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Grupos.FindAsync(modif.Id);

            if (buscado != null)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.EsAdministrador = modif.EsAdministrador == null ? buscado.EsAdministrador : modif.EsAdministrador;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.IdModificador = id;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarGrupo(Guid id, ClaimsPrincipal claims)
    {
        GrupoDTO grp = new GrupoDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarGrupo(grp, claims);
    }
}