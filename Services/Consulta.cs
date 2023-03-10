using DB;
using Entidades;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Herramientas;

namespace Services;

public class Consulta
{
    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <ActividadProyecto> ActividadesProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.ActividadesProyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <ActividadRutaProyecto> ActividadesRutasProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.ActividadesRutasProyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Administrador> Administradores([ScopedService] SSDBContext ctx)
    {
        return ctx.Administradores;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <BitacoraProyecto> BitacorasProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.BitacorasProyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Chat> Chats([ScopedService] SSDBContext ctx)
    {
        return ctx.Chats;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Comentario> Comentarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Comentarios;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Moneda> Monedas([ScopedService] SSDBContext ctx)
    {
        return ctx.Monedas;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Personal> Personal([ScopedService] SSDBContext ctx)
    {
        return ctx.Personal;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Proyecto> Proyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.Proyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Publicacion> Publicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.Publicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <RastreoPublicacion> RastreoPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.RastreoPublicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <RecursoPublicacion> RecursosPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.RecursosPublicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Secuencia> Secuencias([ScopedService] SSDBContext ctx)
    {
        return ctx.Secuencias;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Tabla> Tablas([ScopedService] SSDBContext ctx)
    {
        return ctx.Tablas;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Usuario> Usuarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Usuarios;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <Varios> Varias([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount=true, DefaultPageSize=10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable <VersionApi> VersionesApi([ScopedService] SSDBContext ctx)
    {
        return ctx.VersionesApi;
    }

}
