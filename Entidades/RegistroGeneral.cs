using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("registro_general")]
public class RegistroGeneral
{
    [Column("id")]
    [Key]
    public int Id { get; set; }
    [Column("id_tabla")]
    [MaxLength(10)]
    public string IdTabla { get; set; } = "";
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("referencia")]
    [MaxLength(50)]
    public string Referencia { get; set; } = "";
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
    public virtual Tabla? Tabla { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
}
