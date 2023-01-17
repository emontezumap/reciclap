using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class PaisService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public PaisService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Pais>> TodosLosPaises()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Paises.ToListAsync<Pais>();
        }
    }

    public async Task<Pais?> PaisPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Paises.FindAsync(id);
        }
    }

    public async Task<Pais> CrearPais(Pais nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Paises.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarPais(Pais modif)
    {
        var buscado = await PaisPorId(modif.Id);

        if (buscado != null)
        {
            //     buscado.Nombre = modif.Nombre;
            //     buscado.IdModificador = modif.IdModificador;
            //     buscado.FechaModificacion = DateTime.UtcNow;

            using (var ctx = ctxFactory.CreateDbContext())
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarPais(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await PaisPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarPais(buscado);
            }

            return false;
        }
    }
}