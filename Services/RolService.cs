using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

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

    public async Task<Rol?> UnRol(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Roles.FindAsync(id);
        }
    }

    public async Task<Rol> CrearRol(RolDTO nuevo)
    {
        Rol rol = new Rol()
        {
            Activo = nuevo.Activo,
            Descripcion = nuevo.Descripcion,
            EsCreador = (bool)nuevo.EsCreador,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            // IdCreador = 
            // IdModificador = 
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Roles.Add(rol);
            await ctx.SaveChangesAsync();
        }

        return rol;
    }

    public async Task<bool> ModificarRol(RolDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Roles.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.EsCreador = modif.EsCreador == null ? buscado.EsCreador : (bool)modif.EsCreador;
                buscado.FechaModificacion = DateTime.UtcNow;
                // buscado.IdCreador = 
                // buscado.IdModificador = 

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarRol(Guid id)
    {
        RolDTO rol = new RolDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarRol(rol);
    }
}