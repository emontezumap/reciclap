using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("detalle_publicaciones")]
public class DetallePublicacion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("id_publicacion")]
    public Guid IdPublicacion { get; set; }
    [Column("id_articulo")]
    public Guid IdArticulo { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("cantidad")]
    public int Cantidad { get; set; } = 1;
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
    public virtual Articulo? Articulo { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
}