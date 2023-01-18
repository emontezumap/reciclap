using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

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

    public async Task<Profesion?> UnaProfesion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Profesiones.FindAsync(id);
        }
    }

    public async Task<Profesion> CrearProfesion(ProfesionDTO nuevo)
    {
        Profesion prof = new Profesion()
        {
            Activo = nuevo.Activo,
            Descripcion = nuevo.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid()
            // IdCreador = 
            // IdModificador = 
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Profesiones.Add(prof);
            await ctx.SaveChangesAsync();
        }

        return prof;
    }

    public async Task<bool> ModificarProfesion(ProfesionDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Profesiones.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.FechaModificacion = DateTime.UtcNow;
                // buscado.IdModificador = 

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarProfesion(Guid id)
    {
        ProfesionDTO prof = new ProfesionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarProfesion(prof);
    }
}