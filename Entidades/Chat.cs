using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("chats")]
public class Chat
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("id_publicacion")]
    public Guid IdPublicacion { get; set; }
    [Column("titulo")]
    [MaxLength(300)]
    public string Titulo { get; set; } = "";
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("creado_por")]
    public Guid CreadoPor { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("modificado_por")]
    public Guid ModificadoPor { get; set; }
    [Column("ultima_modificacion")]
    public DateTime UltimaModificacion { get; set; } = DateTime.UtcNow;
    public virtual ICollection<Comentario>? Comentarios { get; set; }
}