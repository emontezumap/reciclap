using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("rastreo_publicaciones")]
public class RastreoPublicacion
{
    [Column("Id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column("id_publicacion")]
    public Guid IdPublicacion { get; set; }
    [Column("estado")]
    public int Estado { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }
    [Column("tiempo_estimado")]
    public long TiempoEstimado { get; set; } = 0;
    [Column("estado_previo")]
    public int EstadoPrevio { get; set; }
    [Column("siguiente_estado")]
    public int SiguienteEstado { get; set; }
    [Column("comentarios")]
    [MaxLength(100)]
    public string Comentarios { get; set; } = "";
    [Column("id_creador")]
    public Guid IdCreador { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("id_modificador")]
    public Guid IdModificador { get; set; }
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    [Column("activo")]
    public bool? Activo { get; set; } = true;

    [JsonIgnore]
    public virtual Publicacion? Publicacion { get; set; }
    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
}