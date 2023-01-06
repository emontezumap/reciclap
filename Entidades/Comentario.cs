using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("comentarios")]
public class Comentario
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("id_chat")]
    public Guid IdChat { get; set; }
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("comentario")]
    public string Texto { get; set; } = "";
    [Column("id_comentario")]
    public Guid? IdComentario { get; set; }
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
    public virtual ICollection<Comentario>? Citas { get; set; }
}