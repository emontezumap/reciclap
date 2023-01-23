using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class Consulta
{
    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Chat> Chats([ScopedService] SSDBContext ctx)
    {
        return ctx.Chats;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Ciudad> Ciudades([ScopedService] SSDBContext ctx)
    {
        return ctx.Ciudades;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Comentario> Comentarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Comentarios;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Estado> Estados([ScopedService] SSDBContext ctx)
    {
        return ctx.Estados;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<EstatusPublicacion> EstatusPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.EstatusPublicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Grupo> Grupos([ScopedService] SSDBContext ctx)
    {
        return ctx.Grupos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Pais> Paises([ScopedService] SSDBContext ctx)
    {
        return ctx.Paises;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Personal> Personal([ScopedService] SSDBContext ctx)
    {
        return ctx.Personal;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Profesion> Profesiones([ScopedService] SSDBContext ctx)
    {
        return ctx.Profesiones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Publicacion> Publicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.Publicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Rol> Roles([ScopedService] SSDBContext ctx)
    {
        return ctx.Roles;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<TipoPublicacion> TiposPublicacion([ScopedService] SSDBContext ctx)
    {
        return ctx.TiposPublicacion;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Usuario> Usuarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Usuarios;
    }
}
