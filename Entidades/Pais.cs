using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("paises")]
public class Pais
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(50)]
    public string Nombre { get; set; } = "";
    [Column("id_creador")]
    public Guid? IdCreador { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("id_modificador")]
    public Guid? IdModificador { get; set; }
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    [Column("activo")]
    public bool? Activo { get; set; } = true;

    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
    public virtual ICollection<Estado>? Estados { get; set; }
}