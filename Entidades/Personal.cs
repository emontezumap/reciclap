using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("personal")]
public class Personal
{
    [Column("id_publicacion")]
    public Guid IdPublicacion { get; set; }
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("id_rol")]
    public Guid IdRol { get; set; }
    [Column("id_creador")]
    public Guid IdCreador { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("id_modificador")]
    public Guid IdModificador { get; set; }
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    [Column("activo")]
    public bool Activo { get; set; } = true;

    public virtual Usuario Creador { get; set; }
    public virtual Usuario Modificador { get; set; }
    public virtual Publicacion Publicacion { get; set; } = new Publicacion();
    public virtual Usuario Usuario { get; set; } = new Usuario();
    public virtual Rol RolLink { get; set; } = new Rol();
}