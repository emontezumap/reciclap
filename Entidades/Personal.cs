using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
    [JsonIgnore]
    public virtual Publicacion Publicacion { get; set; } = new Publicacion();
    [JsonIgnore]
    public virtual Usuario Usuario { get; set; } = new Usuario();
    [JsonIgnore]
    public virtual Rol RolLink { get; set; } = new Rol();
}