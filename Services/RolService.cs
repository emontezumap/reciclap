using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class RolService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public RolService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Rol>> TodosLosRoles()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Roles.ToListAsync<Rol>();
        }
    }

    public async Task<Rol?> RolPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Roles.FindAsync(id);
        }
    }

    public async Task<Rol> CrearRol(Rol nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Roles.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarRol(Rol modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await RolPorId(modif.Id);

            if (buscado != null)
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarRol(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await RolPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarRol(buscado);
            }

            return false;
        }
    }
}