using Entidades;

namespace Services;

public class ConsultaService
{
    private readonly SSDBContext ctx;

    public ConsultaService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Chat> Chats() => ctx.Chats;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Ciudad> Ciudades() => ctx.Ciudades;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Comentario> Comentarios() => ctx.Comentarios;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Estado> Estados() => ctx.Estados;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<EstatusPublicacion> EstatusPublicaciones() => ctx.EstatusPublicaciones;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Grupo> Grupos() => ctx.Grupos;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Pais> Paises() => ctx.Paises;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Personal> Personal() => ctx.Personal;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Profesion> Profesiones() => ctx.Profesiones;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Publicacion> Publicaciones() => ctx.Publicaciones;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Rol> Roles() => ctx.Roles;

    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<TipoPublicacion> TiposPublicacion() => ctx.TiposPublicacion;

    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<Usuario> Usuarios() => ctx.Usuarios;
}
