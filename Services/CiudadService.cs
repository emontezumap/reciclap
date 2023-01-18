using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

namespace Services;

[ExtendObjectType("Mutacion")]
public class CiudadService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public CiudadService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Ciudad>> TodasLasCiudades()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.ToListAsync<Ciudad>();
        }
    }

    public async Task<Ciudad?> UnaCiudad(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.FindAsync(id);
        }
    }

    public async Task<Ciudad> CrearCiudad(CiudadDTO nuevo)
    {
        Ciudad ciudad = new Ciudad()
        {
            Activo = nuevo.Activo,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            // ciudad.IdCreador = 
            IdEstado = (Guid)nuevo.IdEstado,
            // ciudad.IdModificador
            Nombre = (string)nuevo.Nombre
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Ciudades.Add(ciudad);
            await ctx.SaveChangesAsync();
        }

        return ciudad;
    }

    public async Task<bool> ModificarCiudad(CiudadDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Ciudades.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.IdEstado = modif.IdEstado == null ? buscado.IdEstado : (Guid)modif.IdEstado;
                // buscado.IdModificador = 
                buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarCiudad(Guid id)
    {
        CiudadDTO ciudad = new CiudadDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarCiudad(ciudad);
    }
}