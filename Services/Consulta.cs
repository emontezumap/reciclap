// using Microsoft.EntityFrameworkCore;
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
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<ActividadProyecto> ActividadesProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.ActividadesProyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<ActividadRutaProyecto> ActividadesRutasProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.ActividadesRutasProyectos;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<BitacoraProyecto> BitacorasProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.BitacorasProyectos;
    }

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
    public IQueryable<Varios> Ciudades([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
          .Where(p => p.IdTabla == TABLA.CIUDADES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> ClasesPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
          .Where(p => p.IdTabla == TABLA.CLASES_PUBLICACIONES);
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
    public IQueryable<Varios> Estados([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
          .Where(p => p.IdTabla == TABLA.ESTADOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> EstatusProyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.ESTATUS_PROYECTOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> EstatusPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.ESTATUS_PUBLICACIONES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> EstatusRecursos([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.ESTATUS_RECURSOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> FasesPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.FASES_PUBLICACIONES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> GruposUsuarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
          .Where(p => p.IdTabla == TABLA.GRUPOS_USUARIOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Moneda> Monedas([ScopedService] SSDBContext ctx)
    {
        return ctx.Monedas;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> Paises([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
          .Where(p => p.IdTabla == TABLA.PAISES);
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
    public IQueryable<Varios> Profesiones([ScopedService] SSDBContext ctx)
    {
        // return ctx.Profesiones;
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.PROFESIONES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Proyecto> Proyectos([ScopedService] SSDBContext ctx)
    {
        return ctx.Proyectos;
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
    public IQueryable<RastreoPublicacion> RastreoPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.RastreoPublicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<RecursoPublicacion> RecursosPublicaciones([ScopedService] SSDBContext ctx)
    {
        return ctx.RecursosPublicaciones;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> Roles([ScopedService] SSDBContext ctx)
    {
        // return ctx.Roles;
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.ROLES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> RutasProyectos([ScopedService] SSDBContext ctx)
    {
        // return ctx.Roles;
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.RUTAS_PROYECTOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Secuencia> Secuencias([ScopedService] SSDBContext ctx)
    {
        return ctx.Secuencias;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Tabla> Tablas([ScopedService] SSDBContext ctx)
    {
        return ctx.Tablas;
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposActividades([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_ACTIVIDADES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposBitacoras([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_BITACORAS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposCatalogos([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_CATALOGOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposPublicaciones([ScopedService] SSDBContext ctx)
    {
        // return ctx.TiposPublicacion;
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_PUBLICACIONES);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposRecursos([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_RECURSOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Varios> TiposUsuarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Varias
            .Where(rg => rg.IdTabla == TABLA.TIPOS_USUARIOS);
    }

    [UseDbContext(typeof(SSDBContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Usuario> Usuarios([ScopedService] SSDBContext ctx)
    {
        return ctx.Usuarios;
    }
}
