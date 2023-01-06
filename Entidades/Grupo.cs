using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("grupos")]
public class Grupo
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("descripcion")]
    [MaxLength(100, ErrorMessage = "La descripción del grupo no debe exceder los 100 caracteres")]
    public string Descripcion { get; set; } = "";
    [Column("es_administrador")]
    public bool EsAdministrador { get; set; } = false;
    [Column("id_creador")]
    public Guid? IdCreador { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("id_modificador")]
    public Guid? IdModificador { get; set; }
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    [Column("activo")]
    public bool Activo { get; set; } = true;

    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
    [JsonIgnore]
    public virtual ICollection<Usuario>? Usuarios { get; set; }

}