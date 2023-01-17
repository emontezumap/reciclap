using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class ProfesionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ProfesionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Profesion>> TodasLasProfesiones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Profesiones.ToListAsync<Profesion>();
        }
    }

    public async Task<Profesion?> ProfesionPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Profesiones.FindAsync(id);
        }
    }

    public async Task<Profesion> CrearProfesion(Profesion nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Profesiones.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarProfesion(Profesion modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ProfesionPorId(modif.Id);

            if (buscado != null)
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }

    public async Task<bool> EliminarProfesion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ProfesionPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarProfesion(buscado);
            }

            return false;
        }
    }
}