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
    [Column("creado_por")]
    public Guid CreadoPor { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("modificado_por")]
    public Guid ModificadoPor { get; set; }
    [Column("ultima_modificacion")]
    public DateTime UltimaModificacion { get; set; } = DateTime.UtcNow;

    public virtual Publicacion Publicacion { get; set; } = new Publicacion();
    public virtual Usuario Usuario { get; set; } = new Usuario();
    public virtual Rol RolLink { get; set; } = new Rol();
}