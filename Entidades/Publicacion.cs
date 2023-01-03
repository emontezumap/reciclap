using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("publicaciones")]
public class Publicacion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("titulo")]
    [MaxLength(200, ErrorMessage = "El título de la publicación no debe exceder los 200 caracteres")]
    public string Titulo { get; set; } = "";
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("gustan")]
    public int Gustan { get; set; } = 0;
    [Column("no_gustan")]
    public int NoGustan { get; set; } = 0;
    [Column("id_estatus")]
    public Guid IdEstatus { get; set; }
    [Column("id_tipo_publicacion")]
    public Guid IdTipoPublicacion { get; set; }
    [Column("creado_por")]
    public Guid CreadoPor { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("modificado_por")]
    public Guid ModificadoPor { get; set; }
    [Column("ultima_modificacion")]
    public DateTime UltimaModificacion { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public virtual ICollection<Chat>? Chats { get; set; }

    [JsonIgnore]
    public virtual ICollection<Personal>? PublicacionesLink { get; set; }
}
