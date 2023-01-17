using Microsoft.EntityFrameworkCore;
using Entidades;

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

    public async Task<Grupo?> GrupoPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Grupos.FindAsync(id);
        }
    }

    public async Task<Grupo> CrearGrupo(Grupo nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Grupos.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarGrupo(Grupo modif)
    {
        var buscado = await GrupoPorId(modif.Id);

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

    public async Task<bool> EliminarGrupo(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await GrupoPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarGrupo(buscado);
            }

            return false;
        }
    }
}