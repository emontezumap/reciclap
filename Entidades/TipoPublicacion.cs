using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;
public class TipoPublicacion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("descripcion")]
    [MaxLength(200, ErrorMessage = "La descripción del tipo de publicación no debe exceder los 200 caracteres")]
    public string Descripcion { get; set; } = "";
    [Column("creado_por")]
    public Guid CreadoPor { get; set; } = new Guid();
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("modificado_por")]
    public Guid ModificadoPor { get; set; } = new Guid();
    [Column("ultima_modificacion")]
    public DateTime UltimaModificacion { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public virtual ICollection<Publicacion> PublicacionesLink { get; set; } = new List<Publicacion>();

}