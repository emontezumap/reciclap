using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class EstadoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public EstadoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Estado>> TodosLosEstados()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.ToListAsync<Estado>();
        }
    }

    public async Task<Estado?> EstadoPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.FindAsync(id);
        }
    }

    public async Task<Estado> CrearEstado(Estado nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Estados.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarEstado(Estado modif)
    {
        var buscado = await EstadoPorId(modif.Id);

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

    public async Task<bool> EliminarEstado(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await EstadoPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarEstado(buscado);
            }

            return false;
        }
    }
}