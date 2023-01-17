using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class PublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public PublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            this.ctxFactory = ctxFactory;
        }
    }

    public async Task<IEnumerable<Publicacion>> TodasLasPublicaciones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Publicaciones.ToListAsync<Publicacion>();
        }
    }

    public async Task<Publicacion?> PublicacionPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Publicaciones.FindAsync(id);
        }
    }

    public async Task<Publicacion> CrearPublicacion(Publicacion nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Publicaciones.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarPublicacion(Publicacion modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await PublicacionPorId(modif.Id);

            if (buscado != null)
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await PublicacionPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarPublicacion(buscado);
            }

            return false;
        }
    }
}