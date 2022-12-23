using Microsoft.EntityFrameworkCore;

namespace Entidades;

public class SSDBContext : DbContext
{
    public DbSet<Chat>? Chats { get; set; }
    public DbSet<Ciudad>? Ciudades { get; set; }
    public DbSet<Comentario>? Comentarios { get; set; }
    public DbSet<Estado>? Estados { get; set; }
    public DbSet<EstatusPublicacion>? EstatusPublicaciones { get; set; }
    public DbSet<Pais>? Paises { get; set; }
    public DbSet<Personal>? Personal { get; set; }
    public DbSet<Profesion>? Profesiones { get; set; }
    public DbSet<Publicacion>? Publicaciones { get; set; }
    public DbSet<Rol>? Roles { get; set; }
    public DbSet<Usuario>? Usuarios { get; set; }


    public SSDBContext(DbContextOptions<SSDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Chat>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Ciudad>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Comentario>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Estado>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<EstatusPublicacion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Pais>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Personal>()
            .HasKey(c => new { c.IdPublicacion, c.IdUsuario });

        mb.Entity<Profesion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Publicacion>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Rol>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        mb.Entity<Usuario>()
            .Property(c => c.Id)
            .HasDefaultValueSql("newid()");

        // Relaciones
        mb.Entity<Chat>()
            .HasMany(p => p.Comentarios)
            .WithOne()
            .HasForeignKey(p => p.IdChat)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Ciudad>()
            .HasMany(p => p.Usuarios)
            .WithOne()
            .HasForeignKey(p => p.IdCiudad)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Comentario>()
            .HasMany(p => p.Citas)
            .WithOne()
            .HasForeignKey(p => p.IdComentario)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Estado>()
            .HasMany(p => p.Ciudades)
            .WithOne()
            .HasForeignKey(p => p.IdEstado)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<EstatusPublicacion>()
            .HasMany(p => p.Publicaciones)
            .WithOne()
            .HasForeignKey(p => p.IdEstatus)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Pais>()
            .HasMany(p => p.Estados)
            .WithOne()
            .HasForeignKey(p => p.IdPais)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Personal>()
            .HasOne(p => p.Publicacion)
            .WithMany(p => p.PublicacionesLink)
            .HasForeignKey(p => p.IdPublicacion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Personal>()
            .HasOne(p => p.Usuario)
            .WithMany(p => p.UsuariosLink)
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Profesion>()
            .HasMany(p => p.Usuarios)
            .WithOne()
            .HasForeignKey(p => p.IdProfesion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Publicacion>()
            .HasMany(p => p.Chats)
            .WithOne()
            .HasForeignKey(p => p.IdPublicacion)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Rol>()
            .HasMany(p => p.Asignados)
            .WithOne()
            .HasForeignKey(p => p.IdRol)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Usuario>()
            .HasMany(p => p.Comentarios)
            .WithOne()
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.NoAction);
    }
}