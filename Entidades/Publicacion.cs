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

    public virtual Usuario Creador { get; set; }
    public virtual Usuario Modificador { get; set; }
    [JsonIgnore]
    public virtual ICollection<Chat>? Chats { get; set; }
    [JsonIgnore]
    public virtual ICollection<Personal>? PublicacionesLink { get; set; }
}
