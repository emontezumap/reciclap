using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

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

    public async Task<Publicacion?> UnaPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Publicaciones.FindAsync(id);
        }
    }

    public async Task<Publicacion> CrearPublicacion(PublicacionDTO nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            Publicacion pub = new Publicacion();

            pub.Activo = nuevo.Activo;
            pub.Descripcion = nuevo.Descripcion;
            pub.Fecha = (DateTime)nuevo.Fecha;
            pub.FechaCreacion = DateTime.UtcNow;
            pub.FechaModificacion = pub.FechaCreacion;
            pub.Gustan = (int)nuevo.Gustan;
            pub.Id = Guid.NewGuid();
            // pub.IdCreador = 
            pub.IdEstatus = (Guid)nuevo.IdEstatus;
            pub.IdModificador = pub.IdCreador;
            pub.IdTipoPublicacion = (Guid)nuevo.IdTipoPublicacion;
            pub.NoGustan = (int)nuevo.NoGustan;
            pub.Titulo = nuevo.Titulo;

            ctx.Publicaciones.Add(pub);
            await ctx.SaveChangesAsync();

            return pub;
        }
    }

    public async Task<bool> ModificarPublicacion(PublicacionDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Publicaciones.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.Gustan = modif.Gustan == null ? buscado.Gustan : (int)modif.Gustan;
                buscado.IdEstatus = modif.IdEstatus == null ? buscado.IdEstatus : (Guid)modif.IdEstatus;
                // buscado.IdModificador = 
                buscado.IdTipoPublicacion = modif.IdTipoPublicacion == null ? buscado.IdTipoPublicacion : (Guid)modif.IdTipoPublicacion;
                buscado.NoGustan = modif.NoGustan == null ? buscado.NoGustan : (int)modif.NoGustan;
                buscado.Titulo = modif.Titulo == null ? buscado.Titulo : modif.Titulo;

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarPublicacion(Guid id)
    {
        PublicacionDTO pub = new PublicacionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarPublicacion(pub);
    }
}