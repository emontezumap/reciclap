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
    [Column("creado_por")]
    public Guid CreadoPor { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("modificado_por")]
    public Guid ModificadoPor { get; set; }
    [Column("ultima_modificacion")]
    public DateTime UltimaModificacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Comentario>? Citas { get; set; }
}