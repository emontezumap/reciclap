using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class Consulta
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public Consulta(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Chat> Chats()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Chats;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Ciudad> Ciudades()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Ciudades;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Comentario> Comentarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Comentarios;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Estado> Estados()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Estados;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<EstatusPublicacion> EstatusPublicaciones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.EstatusPublicaciones;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Grupo> Grupos()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Grupos;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Pais> Paises()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Paises;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Personal> Personal()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Personal;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Profesion> Profesiones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Profesiones;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Publicacion> Publicaciones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Publicaciones;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Rol> Roles()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Roles;
        }
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<TipoPublicacion> TiposPublicacion()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.TiposPublicacion;
        }
    }

    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Usuario> Usuarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return ctx.Usuarios;
        }
    }
}
