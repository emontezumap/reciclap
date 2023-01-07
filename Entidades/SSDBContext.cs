using Microsoft.EntityFrameworkCore;

namespace Entidades;

public class SSDBContext : DbContext
{
    public DbSet<Chat>? Chats { get; set; }
    public DbSet<Ciudad>? Ciudades { get; set; }
    public DbSet<Comentario>? Comentarios { get; set; }
    public DbSet<Estado>? Estados { get; set; }
    public DbSet<EstatusPublicacion>? EstatusPublicaciones { get; set; }
    public DbSet<Grupo>? Grupos { get; set; }
    public DbSet<Pais>? Paises { get; set; }
    public DbSet<Personal>? Personal { get; set; }
    public DbSet<Profesion>? Profesiones { get; set; }
    public DbSet<Publicacion>? Publicaciones { get; set; }
    public DbSet<Rol>? Roles { get; set; }
    public DbSet<TipoPublicacion>? TiposPublicacion { get; set; }
    public DbSet<Usuario>? Usuarios { get; set; }


    public SSDBContext(DbContextOptions<SSDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Valores por defecto
        ChatValoresPorDefecto(mb);
        CiudadValoresPorDefecto(mb);
        ComentarioValoresPorDefecto(mb);
        EstadoValoresPorDefecto(mb);
        EstatusPublicacionValoresPorDefecto(mb);
        GrupoValoresPorDefecto(mb);
        PaisValoresPorDefecto(mb);
        PersonalValoresPorDefecto(mb);
        ProfesionValoresPorDefecto(mb);
        PublicacionValoresPorDefecto(mb);
        RolValoresPorDefecto(mb);
        TipoPublicacionValoresPorDefecto(mb);
        UsuarioValoresPorDefecto(mb);

        // Indices
        Indices(mb);

        // Relaciones
        ChatRelaciones(mb);
        CiudadRelaciones(mb);
        ComentarioRelaciones(mb);
        EstadoRelaciones(mb);
        EstatusPublicacionRelaciones(mb);
        GrupoRelaciones(mb);
        PaisRelaciones(mb);
        PersonalRelaciones(mb);
        ProfesionRelaciones(mb);
        PublicacionRelaciones(mb);
        RolRelaciones(mb);
        TipoPublicacionRelaciones(mb);
        UsuarioRelaciones(mb);
    }

    // Valores por defecto
    private void ChatValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Chat>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Chat>()
            .Property(c => c.Titulo)
            .HasDefaultValueSql("''");

        mb.Entity<Chat>()
            .Property(c => c.Fecha)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Chat>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Chat>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Chat>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }

    private void CiudadValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Ciudad>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Ciudad>()
            .Property(c => c.Nombre)
            .HasDefaultValueSql("''");

        mb.Entity<Ciudad>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Ciudad>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Ciudad>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Ciudad>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Ciudad>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }

    private void ComentarioValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Comentario>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Comentario>()
             .Property(c => c.Fecha)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Comentario>()
            .Property(c => c.Texto)
            .HasDefaultValueSql("''");

        mb.Entity<Comentario>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Comentario>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Comentario>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void EstadoValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Estado>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Estado>()
            .Property(c => c.Nombre)
            .HasDefaultValueSql("''");

        mb.Entity<Estado>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Estado>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Estado>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Estado>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Estado>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void EstatusPublicacionValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<EstatusPublicacion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<EstatusPublicacion>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void GrupoValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Grupo>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Grupo>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<Grupo>()
             .Property(c => c.EsAdministrador)
             .HasDefaultValue(0);

        mb.Entity<Grupo>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Grupo>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Grupo>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Grupo>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Grupo>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void PaisValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Pais>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Pais>()
            .Property(c => c.Nombre)
            .HasDefaultValueSql("''");

        mb.Entity<Pais>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Pais>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Pais>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Pais>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Pais>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void PersonalValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Personal>()
             .Property(c => c.Fecha)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Personal>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Personal>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Personal>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Personal>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Personal>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void ProfesionValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Profesion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Profesion>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<Profesion>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Profesion>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Profesion>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Profesion>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Profesion>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }

    private void PublicacionValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Publicacion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Publicacion>()
            .Property(c => c.Titulo)
            .HasDefaultValueSql("''");

        mb.Entity<Publicacion>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<Publicacion>()
             .Property(c => c.Fecha)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Publicacion>()
            .Property(c => c.Gustan)
            .HasDefaultValue(0);

        mb.Entity<Publicacion>()
            .Property(c => c.NoGustan)
            .HasDefaultValue(0);

        mb.Entity<Publicacion>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Publicacion>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Publicacion>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void RolValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Rol>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Rol>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<Rol>()
            .Property(c => c.EsCreador)
            .HasDefaultValue(0);

        mb.Entity<Rol>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Rol>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Rol>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Rol>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Rol>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }
    private void TipoPublicacionValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<TipoPublicacion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<TipoPublicacion>()
            .Property(c => c.Descripcion)
            .HasDefaultValueSql("''");

        mb.Entity<TipoPublicacion>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<TipoPublicacion>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<TipoPublicacion>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<TipoPublicacion>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<TipoPublicacion>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }

    private void UsuarioValoresPorDefecto(ModelBuilder mb)
    {
        mb.Entity<Usuario>()
           .Property(c => c.Id)
           .HasDefaultValueSql("newid()");

        mb.Entity<Usuario>()
            .Property(c => c.Nombre)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Nombre2)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Apellido)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Apellido2)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Perfil)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Direccion)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Telefono)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Telefono2)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
             .Property(c => c.Email)
             .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.Email2)
            .HasDefaultValueSql("''");

        mb.Entity<Usuario>()
            .Property(c => c.MaximoPublicaciones)
            .HasDefaultValue(0);

        mb.Entity<Usuario>()
            .Property(c => c.IdProfesion)
            .HasDefaultValueSql("null");

        mb.Entity<Usuario>()
            .Property(c => c.IdCreador)
            .HasDefaultValueSql("null");

        mb.Entity<Usuario>()
            .Property(c => c.IdModificador)
            .HasDefaultValueSql("null");

        mb.Entity<Usuario>()
             .Property(c => c.FechaCreacion)
             .HasDefaultValueSql("getutcdate()");

        mb.Entity<Usuario>()
            .Property(c => c.FechaModificacion)
            .HasDefaultValueSql("getutcdate()");

        mb.Entity<Usuario>()
            .Property(c => c.Activo)
            .HasDefaultValue(1);
    }

    // Indices
    private void Indices(ModelBuilder mb)
    {
        mb.Entity<Chat>()
            .HasIndex(p => p.Titulo).IsUnique();

        mb.Entity<Ciudad>()
            .HasIndex(p => p.Nombre).IsUnique();

        mb.Entity<Estado>()
            .HasIndex(p => p.Nombre).IsUnique();

        mb.Entity<EstatusPublicacion>()
            .HasIndex(p => p.Descripcion).IsUnique();

        mb.Entity<Grupo>()
           .HasIndex(p => p.Descripcion).IsUnique();

        mb.Entity<Pais>()
            .HasIndex(p => p.Nombre).IsUnique();

        mb.Entity<Personal>()
           .HasKey(c => new { c.IdPublicacion, c.IdUsuario });

        mb.Entity<Personal>()
            .HasIndex(p => new { p.IdPublicacion, p.IdRol }).IsUnique();

        mb.Entity<Profesion>()
            .HasIndex(p => p.Descripcion).IsUnique();

        mb.Entity<Publicacion>()
            .HasIndex(p => p.Titulo).IsUnique();

        mb.Entity<Rol>()
            .HasIndex(p => p.Descripcion).IsUnique();

        mb.Entity<TipoPublicacion>()
            .HasIndex(p => p.Descripcion).IsUnique();

        mb.Entity<Usuario>()
            .HasIndex(p => new { p.Nombre, p.Nombre2, p.Apellido, p.Apellido2 }).IsUnique();

        mb.Entity<Usuario>()
            .HasIndex(p => p.Email).IsUnique();
    }

    // Relaciones
    private void ChatRelaciones(ModelBuilder mb)
    {
        mb.Entity<Chat>()
            .HasMany(p => p.Comentarios)
            .WithOne()
            .HasForeignKey(p => p.IdChat)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Chat>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Chat>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void CiudadRelaciones(ModelBuilder mb)
    {
        mb.Entity<Ciudad>()
            .HasMany(p => p.Usuarios)
            .WithOne()
            .HasForeignKey(p => p.IdCiudad)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Ciudad>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Ciudad>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ComentarioRelaciones(ModelBuilder mb)
    {
        mb.Entity<Comentario>()
            .HasMany(p => p.Citas)
            .WithOne()
            .HasForeignKey(p => p.IdComentario)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Comentario>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Comentario>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void EstadoRelaciones(ModelBuilder mb)
    {
        mb.Entity<Estado>()
            .HasMany(p => p.Ciudades)
            .WithOne()
            .HasForeignKey(p => p.IdEstado)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Estado>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Estado>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void EstatusPublicacionRelaciones(ModelBuilder mb)
    {
        mb.Entity<EstatusPublicacion>()
            .HasMany(p => p.Publicaciones)
            .WithOne()
            .HasForeignKey(p => p.IdEstatus)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<EstatusPublicacion>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<EstatusPublicacion>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void GrupoRelaciones(ModelBuilder mb)
    {
        mb.Entity<Grupo>()
            .HasMany(p => p.Usuarios)
            .WithOne()
            .HasForeignKey(p => p.IdGrupo)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Grupo>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Grupo>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void PaisRelaciones(ModelBuilder mb)
    {
        mb.Entity<Pais>()
            .HasMany(p => p.Estados)
            .WithOne()
            .HasForeignKey(p => p.IdPais)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Pais>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Pais>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void PersonalRelaciones(ModelBuilder mb)
    {
        mb.Entity<Personal>()
            .HasOne(p => p.Publicacion)
            .WithMany(p => p.UsuariosLink)
            .HasForeignKey(p => p.IdPublicacion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Personal>()
            .HasOne(p => p.Usuario)
            .WithMany(p => p.PublicacionesLink)
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Personal>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Personal>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ProfesionRelaciones(ModelBuilder mb)
    {
        mb.Entity<Profesion>()
            .HasMany(p => p.Usuarios)
            .WithOne()
            .HasForeignKey(p => p.IdProfesion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Profesion>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Profesion>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void PublicacionRelaciones(ModelBuilder mb)
    {
        mb.Entity<Publicacion>()
            .HasMany(p => p.Chats)
            .WithOne()
            .HasForeignKey(p => p.IdPublicacion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Publicacion>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Publicacion>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void RolRelaciones(ModelBuilder mb)
    {
        // mb.Entity<Rol>()
        //     .HasMany(p => p.Asignados)
        //     .WithOne()
        //     .HasForeignKey(p => p.IdRol)
        //     .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Rol>()
            .HasMany(p => p.RolesAsignados)
            .WithOne()
            .HasForeignKey(p => p.IdRol)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Rol>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Rol>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void TipoPublicacionRelaciones(ModelBuilder mb)
    {
        mb.Entity<TipoPublicacion>()
            .HasMany(p => p.Publicaciones)
            .WithOne()
            .HasForeignKey(p => p.IdTipoPublicacion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<TipoPublicacion>()
           .HasOne(p => p.Creador)
           .WithMany()
           .HasForeignKey(p => p.IdCreador)
           .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<TipoPublicacion>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void UsuarioRelaciones(ModelBuilder mb)
    {
        mb.Entity<Usuario>()
            .HasMany(p => p.Comentarios)
            .WithOne()
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Usuario>()
            .HasOne(p => p.Creador)
            .WithMany()
            .HasForeignKey(p => p.IdCreador)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Usuario>()
            .HasOne(p => p.Modificador)
            .WithMany()
            .HasForeignKey(p => p.IdModificador)
            .OnDelete(DeleteBehavior.NoAction);
    }
}