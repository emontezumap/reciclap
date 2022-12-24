using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public virtual ICollection<Comentario>? Citas { get; set; }
}